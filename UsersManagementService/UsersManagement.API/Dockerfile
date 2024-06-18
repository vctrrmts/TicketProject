#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UsersManagementService/UsersManagement.API/UsersManagement.API.csproj", "UsersManagementService/UsersManagement.API/"]
COPY ["UsersManagementService/UsersManagement.Application/UsersManagement.Application.csproj", "UsersManagementService/UsersManagement.Application/"]
COPY ["UsersManagementService/UsersManagement.Domain/UsersManagement.Domain.csproj", "UsersManagementService/UsersManagement.Domain/"]
COPY ["UsersManagementService/UsersManagement.Infrastructure/UsersManagement.Infrastructure.csproj", "UsersManagementService/UsersManagement.Infrastructure/"]
RUN dotnet restore "./UsersManagementService/UsersManagement.API/UsersManagement.API.csproj"
COPY . .
WORKDIR "/src/UsersManagementService/UsersManagement.API"
RUN dotnet build "./UsersManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UsersManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersManagement.API.dll"]