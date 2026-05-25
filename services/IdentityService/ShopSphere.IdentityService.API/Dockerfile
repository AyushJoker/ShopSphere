# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj files
COPY ["ShopSphere.IdentityService.API/ShopSphere.IdentityService.API.csproj", "ShopSphere.IdentityService.API/"]
COPY ["ShopSphere.IdentityService.Application/ShopSphere.IdentityService.Application.csproj", "ShopSphere.IdentityService.Application/"]
COPY ["ShopSphere.IdentityService.Domain/ShopSphere.IdentityService.Domain.csproj", "ShopSphere.IdentityService.Domain/"]
COPY ["ShopSphere.IdentityService.Infrastructure/ShopSphere.IdentityService.Infrastructure.csproj", "ShopSphere.IdentityService.Infrastructure/"]

# Restore packages
RUN dotnet restore "ShopSphere.IdentityService.API/ShopSphere.IdentityService.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.IdentityService.API"

# Build
RUN dotnet build "ShopSphere.IdentityService.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.IdentityService.API.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.IdentityService.API.dll"]