#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicketEventSearchService/TicketEventSearch.API/TicketEventSearch.API.csproj", "TicketEventSearchService/TicketEventSearch.API/"]
COPY ["TicketEventSearchService/TicketEventSearch.Application/TicketEventSearch.Application.csproj", "TicketEventSearchService/TicketEventSearch.Application/"]
COPY ["TicketEventSearchService/TicketEventSearch.Domain/TicketEventSearch.Domain.csproj", "TicketEventSearchService/TicketEventSearch.Domain/"]
COPY ["TicketEventSearchService/TicketEventSearch.Infrastructure/TicketEventSearch.Infrastructure.csproj", "TicketEventSearchService/TicketEventSearch.Infrastructure/"]
RUN dotnet restore "./TicketEventSearchService/TicketEventSearch.API/TicketEventSearch.API.csproj"
COPY . .
WORKDIR "/src/TicketEventSearchService/TicketEventSearch.API"
RUN dotnet build "./TicketEventSearch.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TicketEventSearch.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketEventSearch.API.dll"]