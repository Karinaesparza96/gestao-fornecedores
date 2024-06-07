# Definindo argumentos para as imagens base do runtime e SDK do .NET
ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

# Etapa base: utilizando a imagem do runtime do ASP.NET Core
FROM ${DOTNET_RUNTIME} AS base
# Configurando a aplicação para escutar na porta 8080
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 8080

# Etapa de construção: utilizando a imagem do SDK do .NET
FROM ${DOTNET_SDK} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["DevApiAspNet.sln", "./"]
COPY ["src/Dev.Api/Dev.Api.csproj", "src/Dev.Api/"]
COPY ["src/Dev.Business/Dev.Business.csproj", "src/Dev.Business/"]
COPY ["src/Dev.Data/Dev.Data.csproj", "src/Dev.Data/"]

RUN dotnet restore 

COPY . .
RUN dotnet build "src/Dev.Api/Dev.Api.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS migrations
RUN dotnet tool install --global dotnet-ef --version 8.0.3
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project src/Dev.Data/ --startup-project src/Dev.Api/

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Dev.Api/Dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: utilizando a imagem base do runtime do ASP.NET Core
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dev.Api.dll"]
