using System;
using System.IO;

namespace Generator
{
    internal static class Program
    {
        private static readonly string[] LtsVersions;
        private static readonly string[] SupportedVersions;
        private static readonly string[] AllVersions;

        static Program()
        {
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
                var version = file.Name.Split('-')[1];
                AllVersions[i++] = version;
            }
        }
        
        private static void Main()
        {
            
        }
    }
}
