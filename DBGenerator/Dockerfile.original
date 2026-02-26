# Stage bazowy (bez zmian)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build (bez zmian)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DBGenerator.csproj", "."]
RUN dotnet restore "./DBGenerator.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./DBGenerator.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish (bez zmian)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DBGenerator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "watch", "run", "--project", "/app/DBGenerator.csproj"]
