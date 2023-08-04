ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-bullseye-2022.04
LABEL maintainer="Vova Lantsov"
LABEL contact_email="contact@vova-lantsov.dev"
LABEL telegram="https://t.me/vova_lantsov"

# Enable detection of running in a container
ENV DOTNET_RUNNING_IN_CONTAINER=true \
# Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
# Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN [ "cross-build-start" ]

RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates libc6 libgcc1 libgssapi-krb5-2 libstdc++6 zlib1g \
    libicu67 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/8.0.100-preview.6.23330.14/dotnet-sdk-8.0.100-preview.6.23330.14-linux-arm.tar.gz \
    && dotnet_sha512='bffe374b1a16e285ef5eae8f9ab89fb7e8fb576c7f9e1853f42561297dc7edc2280301ccf3d9f909c8468bdfa9064bdc9a05946e539cf9b7084133cf29dca6f1' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help

RUN [ "cross-build-end" ]