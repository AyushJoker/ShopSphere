# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj files
COPY ["ShopSphere.ProductService.API/ShopSphere.ProductService.API.csproj", "ShopSphere.ProductService.API/"]
COPY ["ShopSphere.ProductService.Application/ShopSphere.ProductService.Application.csproj", "ShopSphere.ProductService.Application/"]
COPY ["ShopSphere.ProductService.Domain/ShopSphere.ProductService.Domain.csproj", "ShopSphere.ProductService.Domain/"]
COPY ["ShopSphere.ProductService.Infrastructure/ShopSphere.ProductService.Infrastructure.csproj", "ShopSphere.ProductService.Infrastructure/"]

# Restore
RUN dotnet restore "ShopSphere.ProductService.API/ShopSphere.ProductService.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.ProductService.API"

# Build
RUN dotnet build "ShopSphere.ProductService.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.ProductService.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.ProductService.API.dll"]