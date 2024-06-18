#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationService/Notification.API/Notification.API.csproj", "NotificationService/Notification.API/"]
COPY ["NotificationService/Notification.Application/Notification.Application.csproj", "NotificationService/Notification.Application/"]
COPY ["NotificationService/Notification.Domain/Notification.Domain.csproj", "NotificationService/Notification.Domain/"]
COPY ["NotificationService/Notification.Infrastructure/Notification.Infrastructure.csproj", "NotificationService/Notification.Infrastructure/"]
RUN dotnet restore "./NotificationService/Notification.API/Notification.API.csproj"
COPY . .
WORKDIR "/src/NotificationService/Notification.API"
RUN dotnet build "./Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.API.dll"]