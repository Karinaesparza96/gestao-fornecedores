#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Dev.Api/Dev.Api.csproj", "Dev.Api/"]
RUN dotnet restore "Dev.Api/Dev.Api.csproj"
COPY . .
WORKDIR "/src/Dev.Api"
RUN dotnet build "Dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

## Run migrations
FROM buildbase as migrations
RUN dotnet tool install --version 8.0.3 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project src/Dev.Data/ --startup-project src/Dev.Api/

FROM base AS final
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dev.Api.dll"]
