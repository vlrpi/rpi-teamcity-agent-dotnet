ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-bullseye-2023.05
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
    libicu67 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.412/dotnet-sdk-6.0.412-linux-arm64.tar.gz \
    && dotnet_sha512='669c19665f657de00fda41e5ffff8d80395618dc1cc7d6ec50cd06668b135b5fcca193919172c65394210b7c060eae146fd6d8d57deed889ca8fafddca66d06d' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/7.0.306/dotnet-sdk-7.0.306-linux-arm64.tar.gz \
    && dotnet_sha512='1500927cd2b1e048de8ee5339937fd41073a85a82b7a175220a411212d22e4906b4e5e6d29b51d068157d2ecde33238d540508c700793dca8b04b4d1dcd5c89e' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help

RUN [ "cross-build-end" ]