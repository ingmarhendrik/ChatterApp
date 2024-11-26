# Use .NET runtime as the base image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
USER app

# Use .NET SDK as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .

RUN dotnet restore

# Build the project
RUN dotnet build "ConsoleChatApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project for deployment
FROM build AS publish
RUN dotnet publish "ConsoleChatApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image for running the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Specify the entry point for the container
ENTRYPOINT ["dotnet", "ConsoleChatApp.dll"]
