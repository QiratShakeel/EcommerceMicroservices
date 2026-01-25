FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file(s)
COPY src/CatalogService/Catalog.API/*.csproj Catalog.API/
COPY src/CatalogService/Catalog.Application/*.csproj Catalog.Application/
COPY src/CatalogService/Catalog.Infrastructure/*.csproj Catalog.Infrastructure/
COPY src/CatalogService/Catalog.Domain/*.csproj Catalog.Domain/

COPY building-blocks/ ../building-blocks/

# Restore only API project
RUN dotnet restore "Catalog.API/Catalog.API.csproj"

# Copy rest of source code
COPY src/CatalogService/ ./CatalogService/

# Publish only API project
WORKDIR /src/CatalogService/Catalog.API
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","Catalog.API.dll"]
