FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR EcommerceMicroservices/

# Copy project file(s)
# COPY src/CatalogService/Catalog.API/*.csproj src/CatalogService/Catalog.API/
# COPY src/CatalogService/Catalog.Application/*.csproj src/CatalogService/Catalog.Application/
# COPY src/CatalogService/Catalog.Infrastructure/*.csproj src/CatalogService/Catalog.Infrastructure/
# COPY src/CatalogService/Catalog.Domain/*.csproj src/CatalogService/Catalog.Domain/

# COPY building-blocks/ building-blocks/
# COPY api-gateway/ api-gateway/
COPY . .
# Restore only API project
RUN dotnet restore "api-gateway/ApiGateway/ApiGateway.csproj"

# Copy rest of source code

# Publish only API project
# WORKDIR EcommerceMicroservices/src/CatalogService/Catalog.API
RUN dotnet publish "api-gateway/ApiGateway/ApiGateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5004
ENTRYPOINT ["dotnet","ApiGateway.dll"]
