# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy csproj files
COPY ["ShopSphere.PaymentService.API/ShopSphere.PaymentService.API.csproj", "ShopSphere.PaymentService.API/"]
COPY ["ShopSphere.PaymentService.Application/ShopSphere.PaymentService.Application.csproj", "ShopSphere.PaymentService.Application/"]
COPY ["ShopSphere.PaymentService.Domain/ShopSphere.PaymentService.Domain.csproj", "ShopSphere.PaymentService.Domain/"]
COPY ["ShopSphere.PaymentService.Infrastructure/ShopSphere.PaymentService.Infrastructure.csproj", "ShopSphere.PaymentService.Infrastructure/"]

# Restore
RUN dotnet restore "ShopSphere.PaymentService.API/ShopSphere.PaymentService.API.csproj"

# Copy everything
COPY . .

WORKDIR "/src/ShopSphere.PaymentService.API"

# Build
RUN dotnet build "ShopSphere.PaymentService.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish

RUN dotnet publish "ShopSphere.PaymentService.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ShopSphere.PaymentService.API.dll"]