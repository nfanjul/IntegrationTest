ARG BUILD_CONFIGURATION=Debug

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG BUILD_CONFIGURATION
WORKDIR /build
COPY ["src/IntegrationTest.Api/*.csproj", "src/IntegrationTest.Api/"]
COPY ["src/IntegrationTest.Entities/*.csproj", "src/IntegrationTest.Entities/"]
COPY ["test/IntegrationTest.IT/*.csproj", "src/IntegrationTest.IT/"]"

RUN dotnet restore "src/IntegrationTest.Api/IntegrationTest.Api.csproj"
COPY . .
RUN dotnet build IntegrationTest.sln -c ${BUILD_CONFIGURATION}

FROM build as functionaltest
ARG BUILD_CONFIGURATION
WORKDIR /build/test/IntegrationTest.IT
