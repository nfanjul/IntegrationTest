ARG BUILD_CONFIGURATION=Debug

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
ARG BUILD_CONFIGURATION
WORKDIR /build
COPY ["src/IntegrationTest.Api/*.csproj", "src/IntegrationTest.Api/"]
COPY ["src/IntegrationTest.Entities/*.csproj", "src/IntegrationTest.Entities/"]
COPY ["test/IntegrationTest.IT/*.csproj", "src/IntegrationTest.IT/"]"

RUN dotnet restore "src/IntegrationTest.Api/IntegrationTest.Api.csproj"
COPY . .
RUN dotnet build IntegrationTest.sln -c ${BUILD_CONFIGURATION}

FROM build as functionaltest

WORKDIR /build/test/IntegrationTest.IT

FROM build AS publish
ARG BUILD_CONFIGURATION

WORKDIR "/build/src/IntegrationTest.Api"
RUN dotnet publish "IntegrationTest.Api.csproj" -c ${BUILD_CONFIGURATION} -o /app --no-build

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IntegrationTest.Api.dll"]
