#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicketControlService/TicketControlService.API/TicketControlService.API.csproj", "TicketControlService/TicketControlService.API/"]
COPY ["TicketControlService/TicketControlService.Domain/TicketControlService.Domain.csproj", "TicketControlService/TicketControlService.Domain/"]
COPY ["TicketControlService/TicketControlService.Infrastructure/TicketControlService.Infrastructure.csproj", "TicketControlService/TicketControlService.Infrastructure/"]
COPY ["TicketControlService/TicketControlService/TicketControlService.Application/TicketControlService.Application.csproj", "TicketControlService/TicketControlService/TicketControlService.Application/"]
RUN dotnet restore "./TicketControlService/TicketControlService.API/TicketControlService.API.csproj"
COPY . .
WORKDIR "/src/TicketControlService/TicketControlService.API"
RUN dotnet build "./TicketControlService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TicketControlService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketControlService.API.dll"]