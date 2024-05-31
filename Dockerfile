# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /build

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and publish the app
COPY . . 
RUN dotnet publish --no-restore -o app

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /build/app ./

# Debug step: List files in the app directory
RUN ls -la /app

# Expose port 5000 (or any other port your application listens on)
EXPOSE 5000

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "asd123.dll"]  
# Replace YourApp.dll with the actual name of your project DLL
