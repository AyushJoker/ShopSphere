# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj files
COPY ["ShopSphere.InventoryService.API/ShopSphere.InventoryService.API.csproj", "ShopSphere.InventoryService.API/"]
COPY ["ShopSphere.InventoryService.Application/ShopSphere.InventoryService.Application.csproj", "ShopSphere.InventoryService.Application/"]
COPY ["ShopSphere.InventoryService.Domain/ShopSphere.InventoryService.Domain.csproj", "ShopSphere.InventoryService.Domain/"]
COPY ["ShopSphere.InventoryService.Infrastructure/ShopSphere.InventoryService.Infrastructure.csproj", "ShopSphere.InventoryService.Infrastructure/"]

# Restore
RUN dotnet restore "ShopSphere.InventoryService.API/ShopSphere.InventoryService.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.InventoryService.API"

# Build
RUN dotnet build "ShopSphere.InventoryService.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.InventoryService.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.InventoryService.API.dll"]