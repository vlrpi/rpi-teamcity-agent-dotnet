ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:ubuntu-xenial-2021.2
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.415/dotnet-sdk-3.1.415-linux-arm.tar.gz \
    && dotnet_sha512='b738f0b4455b926b8fafa9699525cebbf51eaad7bbe0e77267640a3fe33645ab4901552008de0a9916f55d0eff1de61c22ebd432a5a1fb5099f0cfa1b33a5e75' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.403/dotnet-sdk-5.0.403-linux-arm.tar.gz \
    && dotnet_sha512='1b7b4a8aea62a7fd1e1919710b2b35b41ae31a64f79b4cc056a3e5f4778bb6cc0b92999321e1632f29a1eb88d734e44fc42eea52ddfb8aa0424cafd9796a503c' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.100/dotnet-sdk-6.0.100-linux-arm.tar.gz \
    && dotnet_sha512='c1e555893c48c4f4256d3e6b1d36b31d8a4d7763a6e958fb63dd31436c660648d481612b5e25d79a613e84a1954f5eac2c9c2b740bf410958172780f7bbeaeb3' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help