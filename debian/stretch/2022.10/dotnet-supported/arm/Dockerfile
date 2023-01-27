ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-stretch-2022.10
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

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/3.1.426/dotnet-sdk-3.1.426-linux-arm.tar.gz \
    && dotnet_sha512='dd357fa62f486cc9c3855230753b34c070d06c8d3e887753866886dda3c931533c780e02f94fe8e73e66ef504e11dd7fb63a004f5914beeccb36b4ce48f50914' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/5.0.408/dotnet-sdk-5.0.408-linux-arm.tar.gz \
    && dotnet_sha512='4f12b2c6cc242666450eb5f892430a956ff34b2ab30aa63ee444565327fbad569152506e153a550b4a45cad6d8346f37ca7bcca68e9d46540c7f170e24b26296' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.405/dotnet-sdk-6.0.405-linux-arm.tar.gz \
    && dotnet_sha512='c35fb905495f6eaf8b26ea6d61bcc59a56da8e7f3ac70dc8e620553d4baea81f65412bbed1099a724b22a118e09c4e290510434ac593d2cf6cf18db7cf370268' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/7.0.102/dotnet-sdk-7.0.102-linux-arm.tar.gz \
    && dotnet_sha512='67db0338992e39b5ae83756d7f89f1e9195b624c24ba08124ee25cc5e3ce4f09af6d5aa45cd301585cd41f9b71e9de1a82d6afa4a16149d1098377e6e44886f8' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help