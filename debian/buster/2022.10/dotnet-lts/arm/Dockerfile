ARG BASE_ARCH

FROM vlrpi/teamcity-agent-${BASE_ARCH}:debian-buster-2022.10
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
    libicu63 libssl1.1 \
    && rm -rf /var/lib/apt/lists/*

RUN curl -SL --output dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/6.0.405/dotnet-sdk-6.0.405-linux-arm.tar.gz \
    && dotnet_sha512='c35fb905495f6eaf8b26ea6d61bcc59a56da8e7f3ac70dc8e620553d4baea81f65412bbed1099a724b22a118e09c4e290510434ac593d2cf6cf18db7cf370268' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -zxf dotnet.tar.gz \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help