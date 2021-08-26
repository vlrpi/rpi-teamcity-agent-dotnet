FROM vlrpi/rpi-teamcity-agent:ubuntu-bionic-2021.1
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/2.1.818/dotnet-sdk-2.1.818-linux-arm64.tar.gz \
    && dotnet_sha512='4a8dfb282ba5c8b286e3b09c100a738a43df41fef371d0f6e7b3341aafb8ff00436658d1139175ec0a545e49a15aedda0efc6a4b97b7324c3cef5cdab8ff0451' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.100-preview.7.21379.14/dotnet-sdk-6.0.100-preview.7.21379.14-linux-arm64.tar.gz \
    && dotnet_sha512='7ee5fbb4e700bcc8a840ec950a764c6ec4cbb8e23394d75a3b4b547e1178f0ae16b0edc262b0fbec5bb9dde3e870225928b147b440f1c27ffada60c03b903dde' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.412/dotnet-sdk-3.1.412-linux-arm64.tar.gz \
    && dotnet_sha512='bfc6f58cb0b87b0a2cf42d91494b914ab0997c91289599cce1704ee33fac9773e5b37b48d40c2d0fc53709c7f94cc37c26533ef7d0eb90f6db05680b2b603da0' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help