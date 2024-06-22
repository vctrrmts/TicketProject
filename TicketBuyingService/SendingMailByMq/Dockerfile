#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicketBuyingService/SendingMailByMq/SendingMailByMq.csproj", "TicketBuyingService/SendingMailByMq/"]
RUN dotnet restore "./TicketBuyingService/SendingMailByMq/SendingMailByMq.csproj"
COPY . .
WORKDIR "/src/TicketBuyingService/SendingMailByMq"
RUN dotnet build "./SendingMailByMq.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN apt-get update && apt-get install -y \
    fontconfig \
    fonts-dejavu-core \
    fonts-freefont-ttf \
    && apt-get clean
RUN fc-cache -f -v

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SendingMailByMq.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SendingMailByMq.dll"]