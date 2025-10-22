# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish PumpTestFactory.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set the entry point
ENTRYPOINT ["dotnet", "PumpTestFactory.dll"]