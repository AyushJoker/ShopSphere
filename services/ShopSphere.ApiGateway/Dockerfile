# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj
COPY ["ShopSphere.ApiGateway/ShopSphere.ApiGateway.csproj", "ShopSphere.ApiGateway/"]

# Restore packages
RUN dotnet restore "ShopSphere.ApiGateway/ShopSphere.ApiGateway.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.ApiGateway"

# Build
RUN dotnet build "ShopSphere.ApiGateway.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.ApiGateway.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.ApiGateway.dll"]