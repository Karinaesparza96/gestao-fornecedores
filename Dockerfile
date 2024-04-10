ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /home/app
EXPOSE 8080

# Base for build
FROM ${DOTNET_SDK} AS build
WORKDIR /source

COPY ["DevApiAspNet.sln", "./"]
COPY ["src/Dev.Api/Dev.Api.csproj", "src/Dev.Api/Dev.Api.csproj"]
COPY ["src/Dev.Business/Dev.Business.csproj", "src/Dev.Business/Dev.Business.csproj"]
COPY ["src/Dev.Data/Dev.Data.csproj", "src/Dev.Data/Dev.Data.csproj"]

RUN dotnet restore DevApiAspNet.sln

COPY ["src/", "src/"]

## Run migrations
FROM build as migrations
RUN dotnet tool install --version 8.0.3 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project src/Dev.Data/ --startup-project src/Dev.Api/

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Dev.Api/Dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dev.Api.dll"]