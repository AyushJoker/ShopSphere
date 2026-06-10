# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj files
COPY ["ShopSphere.OrderService.API/ShopSphere.OrderService.API.csproj", "ShopSphere.OrderService.API/"]
COPY ["ShopSphere.OrderService.Application/ShopSphere.OrderService.Application.csproj", "ShopSphere.OrderService.Application/"]
COPY ["ShopSphere.OrderService.Domain/ShopSphere.OrderService.Domain.csproj", "ShopSphere.OrderService.Domain/"]
COPY ["ShopSphere.OrderService.Infrastructure/ShopSphere.OrderService.Infrastructure.csproj", "ShopSphere.OrderService.Infrastructure/"]

# Restore
RUN dotnet restore "ShopSphere.OrderService.API/ShopSphere.OrderService.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.OrderService.API"

# Build
RUN dotnet build "ShopSphere.OrderService.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.OrderService.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.OrderService.API.dll"]