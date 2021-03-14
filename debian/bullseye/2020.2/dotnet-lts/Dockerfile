FROM vlrpi/rpi-teamcity-agent:debian-bullseye-2020.2
LABEL maintainer="Vova Lantsov"
LABEL contact_email="contact@vova-lantsov.dev"
LABEL telegram="https://t.me/vova_lantsov"

ENV DOTNET_SDK_2_1_VERSION=2.1.814 \
    DOTNET_SDK_3_1_VERSION=3.1.407 \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        procps \
        wget \
    && rm -rf /var/lib/apt/lists/*

# Install .NET Core 2.1 SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_2_1_VERSION-linux-arm64.tar.gz \
    && dotnet_sha512='2c4a74d6b238ab316e0e60caff77eaeafe5de8fa44fc3b0d828cf81de97d99b065a6260f1e6209f4021be0bc1c9f082a9648c341d65cd36fe3dce1b6eb86ae96' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -oxzf dotnet.tar.gz ./packs ./sdk ./templates ./LICENSE.txt ./ThirdPartyNotices.txt \
    && rm dotnet.tar.gz

# Install .NET Core 3.1 SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_3_1_VERSION-linux-arm64.tar.gz \
    && dotnet_sha512='4ae1c92bd4cdf0ea459591e87aea0e2f560df9d2c406e68dcf6667576ebf7761817683b565d42aeca74a6f03cdb8342d3fd1f9c81a4657a78043a5f765dc549c' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -oxzf dotnet.tar.gz ./packs ./sdk ./templates ./LICENSE.txt ./ThirdPartyNotices.txt \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help