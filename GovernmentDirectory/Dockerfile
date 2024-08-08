# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GovernmentDirectory/GovernmentDirectory.csproj", "GovernmentDirectory/"]
RUN dotnet restore "GovernmentDirectory/GovernmentDirectory.csproj"
COPY . .
WORKDIR "/src/GovernmentDirectory"
RUN dotnet build "GovernmentDirectory.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "GovernmentDirectory.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: use the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GovernmentDirectory.dll"]
