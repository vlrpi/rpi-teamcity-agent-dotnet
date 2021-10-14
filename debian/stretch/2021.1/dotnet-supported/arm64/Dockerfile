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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.414/dotnet-sdk-3.1.414-linux-arm64.tar.gz \
    && dotnet_sha512='42b526d4ae914a0f1b04cbefe70b2c052eae9791dce54431ee5aff2e1bba5dbd08f49505a835319dab0551e9e9788f239e53ac154760cc8c0a85512cbe568408' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.402/dotnet-sdk-5.0.402-linux-arm64.tar.gz \
    && dotnet_sha512='9681da343f72998590541e0ee454a3d68aa4caff761ecc727c837fdbe8645c11487d8e8c4f6f9d3cda35373cee046b4857289a2307aa4929878633d13443e5bf' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.100-rc.2.21505.57/dotnet-sdk-6.0.100-rc.2.21505.57-linux-arm64.tar.gz \
    && dotnet_sha512='14aa96f47f7f3520075e41753c705bdbf7f84fd7cff7cc2add1095a13e3e3c44eaaf2d822551902f05a6ad0c9acd7f7424190b7c09c397004c632eddc8acd5ac' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help