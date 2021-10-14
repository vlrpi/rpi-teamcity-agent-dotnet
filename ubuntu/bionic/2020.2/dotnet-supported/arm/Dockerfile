ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:ubuntu-bionic-2020.2
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.414/dotnet-sdk-3.1.414-linux-arm.tar.gz \
    && dotnet_sha512='08c93b7948151fbe3db9fa105ed02bfefb510e437aa9ad418c60ba96b41a7e25f891a1d4308f51595a9aa385d4e9ca05cab8a803e75728fb70aa9bae30f37b67' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.402/dotnet-sdk-5.0.402-linux-arm.tar.gz \
    && dotnet_sha512='5de1f0efff7f1fab3492f638cbbd1302de99917b6dbba4bf3ebbc0190cc325ed914b6d4494ca6f8cf6c626c6c0fb78ba2b286965e0c03b0f5b298ff3c731f107' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.100-rc.2.21505.57/dotnet-sdk-6.0.100-rc.2.21505.57-linux-arm.tar.gz \
    && dotnet_sha512='2396998b8f21527f979b84460aee8621ca5fa275e4a1ee83b11fe9f12b42479931e56787d7dc82640d72470df185cb02d0bbc5e0e3e920bb6abb2d270051b188' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help