ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-stretch-2021.2
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.416/dotnet-sdk-3.1.416-linux-arm.tar.gz \
    && dotnet_sha512='33a6d64f466839cc30adef87909a2ff98ecdf6bb763b82a7951314ee8eded7dc210297f914d4aa0b9c0b101aa0c33da97cb15ff64c5f83f08b212b885d662e90' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.405/dotnet-sdk-5.0.405-linux-arm.tar.gz \
    && dotnet_sha512='a84eaa3ae75377bf9c6853c58187888324ca412a51bf7f3f96b1fdefb4dbbc02d4d0e71e5e6cbd07b4124684cda054ec14910cbc220d1120845ec6219d7e0636' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.200/dotnet-sdk-6.0.200-linux-arm.tar.gz \
    && dotnet_sha512='c2950d5da671a50c955d07997b288c02076b1d69014d3ac3f5941179e29a67b4d56fc5acbde85fe13ffd46efa95ead05b39d90dc577d2a668f637e6a9547944a' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help