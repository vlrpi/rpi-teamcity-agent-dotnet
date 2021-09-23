using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generator
{
    internal static class Program
    {
        private static readonly string[] LtsVersions;
        private static readonly string[] SupportedVersions;
        private static readonly string[] AllVersions;
        private static readonly string[] TeamcityVersions;
        private static readonly Dictionary<string, (string sdkVersion, string sdkHash)> SdkHashes = new();

        private const string DockerFileTemplate = @"
FROM vlrpi/rpi-teamcity-agent:{osName}-{osVersion}-{teamcityVersion}
LABEL maintainer=""Vova Lantsov""
LABEL contact_email=""contact@vova-lantsov.dev""
LABEL telegram=""https://t.me/vova_lantsov""

    # Enable detection of running in a container
ENV DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates libc6 libgcc1 libgssapi-krb5-2 libstdc++6 zlib1g \
    {packagesToInstall} \
    && rm -rf /var/lib/apt/lists/*

{installCommands} \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help
";

        private const string DockerFileInstallationCommand = @"
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/{sdkVersion}/dotnet-sdk-{sdkVersion}-linux-arm64.tar.gz \
    && dotnet_sha512='{sdkHash}' \
    && echo ""$dotnet_sha512 dotnet.tar.gz"" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
";

        static Program()
        {
            Directory.GetCurrentDirectory();
            
            var ltsText = File.ReadAllText("lts.txt");
            var supportedText = File.ReadAllText("supported.txt");
            LtsVersions = ltsText.Split(',', StringSplitOptions.RemoveEmptyEntries);
            SupportedVersions = supportedText.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var versionFiles = Directory.GetFiles("Versions");
            AllVersions = new string[versionFiles.Length];
            int i = 0;
            foreach (var versionFile in versionFiles)
            {
                var file = new FileInfo(versionFile);
                var version = Path.GetFileNameWithoutExtension(file.Name).Split('-')[1];
                AllVersions[i++] = version;

                using var fileStream = file.OpenRead();
                using var reader = new StreamReader(fileStream);
                string sdkVersion = reader.ReadLine();
                string sdkHash = reader.ReadLine();
                SdkHashes[version] = (sdkVersion, sdkHash);
            }
            
            var teamcityVersionsText = File.ReadAllText("teamcity.txt");
            TeamcityVersions = teamcityVersionsText.Split(',');
        }
        
        private static void Main()
        {
            var supportedOs = new[]
            {
                new
                {
                    system = "debian",
                    info = new (string osVersion, string packagesToInstall)[] {("buster", "libicu63 libssl1.1"),("bullseye", "libicu67 libssl1.1"),("stretch", "libicu57 libssl1.1")}
                },
                new
                {
                    system = "ubuntu",
                    info = new (string osVersion, string packagesToInstall)[] {("bionic", "libicu60 libssl1.1"),("focal", "libicu66 libssl1.1"),("xenial", "libicu55 libssl1.0.0")}
                }
            };
            var now = DateTime.Now;

            foreach (var supportedOsItem in supportedOs)
            {
                string osName = supportedOsItem.system;
                foreach (var osInfo in supportedOsItem.info)
                {
                    var osVersion = osInfo.osVersion;
                    var packagesToInstall = osInfo.packagesToInstall;
                    foreach (var teamcityVersion in TeamcityVersions)
                    {
                        bool isLatest = teamcityVersion == TeamcityVersions[^1];
                        string[] separatedTeamcityVersion = teamcityVersion.Split('.');
                        bool isLatestForYear = TeamcityVersions.Last(v => v.StartsWith(separatedTeamcityVersion[0])) ==
                                               teamcityVersion;
                        
                        string directoryPath = Path.Combine(osName, osVersion, teamcityVersion);
                        var directory = Directory.CreateDirectory(directoryPath);
                        File.Create(Path.Combine(directoryPath, ".exclude")).Dispose();

                        string[] versions = AllVersions.Append("lts").Append("supported").ToArray();
                        foreach (var dotnetVersion in versions)
                        {
                            var subDir = directory.CreateSubdirectory($"dotnet-{dotnetVersion}");
                            using var tagsFile = File.CreateText(Path.Combine(subDir.FullName, ".tags"));
                            if (isLatest)
                                tagsFile.WriteLine($"tc-latest-dotnet-{dotnetVersion}");
                            if (isLatestForYear)
                                tagsFile.WriteLine($"tc-{separatedTeamcityVersion[0]}-dotnet-{dotnetVersion}");
                            tagsFile.WriteLine($"tc-{teamcityVersion}-dotnet-{dotnetVersion}");
                            tagsFile.Write($"tc-{teamcityVersion}-dotnet-{dotnetVersion}-{now:yyyyMMdd}-{now:hhmmss}");

                            string dockerFileContent = GetDockerFile(osName, osVersion, teamcityVersion,
                                dotnetVersion switch
                                {
                                    "lts" => LtsVersions,
                                    "supported" => SupportedVersions,
                                    _ => new[] {dotnetVersion}
                                }, packagesToInstall);
                            using var dockerFile = File.CreateText(Path.Combine(subDir.FullName, "Dockerfile"));
                            dockerFile.Write(dockerFileContent);
                        }
                    }
                }
            }
        }

        private static string GetDockerFile(string osName, string osVersion, string teamcityVersion,
            string[] dotnetVersions, string packagesToInstall)
        {
            var installCommandsBuilder = new StringBuilder();
            installCommandsBuilder.AppendJoin("\n\n",
                SdkHashes.Where(it => dotnetVersions.Contains(it.Key))
                    .OrderBy(it => it.Key)
                    .Select(it => GetInstallationCommand(it.Value.sdkVersion, it.Value.sdkHash)));

            return DockerFileTemplate
                .Replace("{osName}", osName)
                .Replace("{osVersion}", osVersion)
                .Replace("{teamcityVersion}", teamcityVersion)
                .Replace("{installCommands}", installCommandsBuilder.ToString())
                .Replace("{packagesToInstall}", packagesToInstall)
                .Trim();
        }

        private static string GetInstallationCommand(string sdkVersion, string sdkHash)
        {
            return DockerFileInstallationCommand
                .Replace("{sdkVersion}", sdkVersion)
                .Replace("{sdkHash}", sdkHash)
                .Trim();
        }
    }
}
