ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:ubuntu-xenial-2022.10
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
    libicu55 libssl1.0.0 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.408/dotnet-sdk-5.0.408-linux-arm64.tar.gz \
    && dotnet_sha512='50f23d7aca91051d8b7c37f1a76b1eb51e6fe73e017d98558d757a6b9699e4237d401ce81515c1601b8c21eb62fee4e0b4f0bbed8967eefa3ceba75fc242f01b' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help