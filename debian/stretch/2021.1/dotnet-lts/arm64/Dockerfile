ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-stretch-2021.1
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.416/dotnet-sdk-3.1.416-linux-arm64.tar.gz \
    && dotnet_sha512='0065c7afb129b1a0e0c11703309f3b45cf9a3c0ea156247f7cc61555f21c37054f215eb77add509dad77b1d388a4e6c585f8a8016109f31c5b64184b25e2c407' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.200/dotnet-sdk-6.0.200-linux-arm64.tar.gz \
    && dotnet_sha512='4e2b8f65f4cd9d1e54233b377e7efef5202eee3fa5e4592131ff03ecab032bfccdc91f4e8c806064fb769c48f946634bf1e420afecd7fcd2d981d40eeec5a881' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help