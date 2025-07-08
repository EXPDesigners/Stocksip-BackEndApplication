# Dockerfile for StockSip.Platform.API
# Summary: 
# This Dockerfile builds and runs the StocksIP.Platform.API application using .NET 9.0
# Description:
# This Dockerfile is designed to create a Docker image for the StockSip.Platform.API application.
# It uses a multi-stage build process to first compile the application using the .NET SDK,
# and then run it using the .NET runtime. The final image is lightweight and contains only the necessary files to run the application.
# Version: 1.0
# Maintainer: Web Application Team

# Step 1: Build the application
# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
# Set the working directory in the container
WORKDIR /app
# Copy the project files
# Copy the project files and restore dependencies
COPY StockSip.Platform.API/*.csproj StockSip.Platform.API/
# Restore dependencies
RUN dotnet restore ./StockSip.Platform.API
# Copy the rest of the application files
COPY . .

# Step 2: Deploy the application to builder stage
# Publish the application in Release mode
RUN dotnet publish ./StockSip.Platform.API -c Release -o out

# Step 3: Publish to Production and Run the application
# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
# Set the working directory in the container
WORKDIR /app
# Copy the published application from the builder stage
COPY --from=builder /app/out .
EXPOSE 80
# Set EntryPoint to run the application
ENTRYPOINT ["dotnet", "StockSip.Platform.API.dll"]