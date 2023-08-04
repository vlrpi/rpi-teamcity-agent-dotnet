ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:ubuntu-bionic-2023.05
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
    libc6 libgcc1 libgssapi-krb5-2 libstdc++6 \
    libicu60 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.412/dotnet-sdk-6.0.412-linux-arm.tar.gz \
    && dotnet_sha512='4a22174875ae06a48485aa42353ef61e6c424c77d34139bbc2ad5dcbf72bda6625e748af98df54e7677b1b3fc804267b8828ab1c5caa45a693f3f7bf4d6a3306' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help

RUN [ "cross-build-end" ]