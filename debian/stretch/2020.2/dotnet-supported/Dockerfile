FROM vlrpi/rpi-teamcity-agent:debian-stretch-2020.2
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.413/dotnet-sdk-3.1.413-linux-arm64.tar.gz \
    && dotnet_sha512='39f198f07577faf81f09ca621fb749d5aac38fc05e7e6bd6226009679abc7d001454068430ddb34b320901955f42de3951e2707e01bce825b5216df2bc0c8eca' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.401/dotnet-sdk-5.0.401-linux-arm64.tar.gz \
    && dotnet_sha512='770dcf18c08cd285934af61bedc06ffcc16a74115d15376f72376cdfbb4ab9cc9f53537ca1fe5d906b4b3c30b960ffe1404d6f7e01254091b4b9d288e9e972fa' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.100-rc.1.21458.32/dotnet-sdk-6.0.100-rc.1.21458.32-linux-arm64.tar.gz \
    && dotnet_sha512='b27bf9be0198f7fda3eee514c86758665b144e6fc7d2f3e43223e65f80dfc85a43bbc9f61a5e8dbda2e10ec193fafd2c208e64e2e98358f0c80ef6e349bd7c33' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help