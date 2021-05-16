FROM vlrpi/rpi-teamcity-agent:debian-bullseye-2020.1
LABEL maintainer="Vova Lantsov"
LABEL contact_email="contact@vova-lantsov.dev"
LABEL telegram="https://t.me/vova_lantsov"

ENV DOTNET_SDK_2_1_VERSION=2.1.816 \
    DOTNET_SDK_3_1_VERSION=3.1.409 \
    DOTNET_SDK_5_VERSION=5.0.203 \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        ca-certificates \
        libc6 \
        libgcc1 \
        libgssapi-krb5-2 \
        libicu67 \
        libssl1.1 \
        libstdc++6 \
        zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Install .NET Core 2.1 SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_2_1_VERSION/dotnet-sdk-$DOTNET_SDK_2_1_VERSION-linux-arm64.tar.gz \
    && dotnet_sha512='32e72e26062444405f8d94918948ebd3a6eb4c4fa5d5c144fab35e23d2c44c9e4ab0d2f12e70c249b2972d5fef32bcbd2551f7920cd0fe778a38457368d477af' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

# Install .NET Core 3.1 SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_3_1_VERSION/dotnet-sdk-$DOTNET_SDK_3_1_VERSION-linux-arm64.tar.gz \
    && dotnet_sha512='edc011e5ee64fc76e8004aa73d439e7cea922ab00be6c70250c5f73cf6838b1935f5d3d3c9aa65f83bfd3923751bc1a6d92be3fba64a0a09a4acb4fd8d6db4c7' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

# Install .NET 5 SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/$DOTNET_SDK_5_VERSION/dotnet-sdk-$DOTNET_SDK_5_VERSION-linux-arm64.tar.gz \
    && dotnet_sha512='a9d588ff27f62a73a61c1486e26046c5cd9030557d9f929617e779679e670a854e37b85fa3bbb35e768a72cd07d12b90e6ad30ae500f12303e2e9c883d07a8f0' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help