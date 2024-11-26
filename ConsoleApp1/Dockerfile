# Learn more about customizing your container for Visual Studio at:
# https://aka.ms/customizecontainer

# Base image for running the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
USER app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

# Copy project file and restore dependencies
COPY ["ChatServer.csproj", "ConsoleApp1/"]
RUN dotnet restore "ConsoleApp1/ChatServer.csproj"

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet build "ChatServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Expose the application port
EXPOSE 7891

# Final stage: runtime
FROM base AS final
WORKDIR /app

# Copy the published files into the runtime image
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ChatServer.dll"]