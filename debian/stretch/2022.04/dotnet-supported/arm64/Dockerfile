ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-stretch-2022.04
LABEL maintainer="Vova Lantsov"
LABEL contact_email="contact@vova-lantsov.dev"
LABEL telegram="https://t.me/vova_lantsov"

# Enable detection of running in a container
ENV DOTNET_RUNNING_IN_CONTAINER=true \
# Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
# Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates libc6 libgcc1 libgssapi-krb5-2 libstdc++6 zlib1g \
    libicu57 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.426/dotnet-sdk-3.1.426-linux-arm64.tar.gz \
    && dotnet_sha512='300e154fba3123644910bbb89a6e61f67569677efa359aa110871cbbb62afad059709dc362f0af27ece0b9a30bc3e6ef57c3cb7c6f75377b20d48636605f30f7' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.408/dotnet-sdk-5.0.408-linux-arm64.tar.gz \
    && dotnet_sha512='50f23d7aca91051d8b7c37f1a76b1eb51e6fe73e017d98558d757a6b9699e4237d401ce81515c1601b8c21eb62fee4e0b4f0bbed8967eefa3ceba75fc242f01b' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.405/dotnet-sdk-6.0.405-linux-arm64.tar.gz \
    && dotnet_sha512='6c31666a95817a7049bd47717c9cf9ab159e94e90987f46883e272dc6dee92fb0d890f4e590faca4458cd2b3943133fb2fa58c2fc175db98d4c6c531f6b2c3c3' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/7.0.102/dotnet-sdk-7.0.102-linux-arm64.tar.gz \
    && dotnet_sha512='a98abed737214bd61266d1a5d5096ae34537c6bef04696670d88684e9783bab6f6d45823f775648d723c4e031b1bd341f771baa6b265d2b6e5f5158213721627' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help