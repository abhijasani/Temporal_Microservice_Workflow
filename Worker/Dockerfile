# Use the official .NET Core SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj file(s) and restore dependencies
COPY Worker/Worker.csproj ./
RUN dotnet restore

# Copy the entire project and build the application
COPY ./Worker ./
RUN dotnet publish -c Release -o out

# Use the official .NET Core runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/out .

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "Worker.dll"]
