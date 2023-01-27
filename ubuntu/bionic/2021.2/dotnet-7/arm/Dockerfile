ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:ubuntu-bionic-2021.2
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
    libicu60 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/7.0.102/dotnet-sdk-7.0.102-linux-arm.tar.gz \
    && dotnet_sha512='67db0338992e39b5ae83756d7f89f1e9195b624c24ba08124ee25cc5e3ce4f09af6d5aa45cd301585cd41f9b71e9de1a82d6afa4a16149d1098377e6e44886f8' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help