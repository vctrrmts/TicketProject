#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthorizationService/Authorization.API/Authorization.API.csproj", "AuthorizationService/Authorization.API/"]
COPY ["AuthorizationService/Authorization.Application/Authorization.Application.csproj", "AuthorizationService/Authorization.Application/"]
COPY ["AuthorizationService/Authorization.Domain/Authorization.Domain.csproj", "AuthorizationService/Authorization.Domain/"]
COPY ["AuthorizationService/Authorization.Infrastructure/Authorization.Infrastructure.csproj", "AuthorizationService/Authorization.Infrastructure/"]
RUN dotnet restore "./AuthorizationService/Authorization.API/Authorization.API.csproj"
COPY . .
WORKDIR "/src/AuthorizationService/Authorization.API"
RUN dotnet build "./Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authorization.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization.API.dll"]