FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project folders
COPY src/OrderService/OrderService.API/ OrderService.API/
COPY src/OrderService/OrderService.Application/ OrderService.Application/
COPY src/OrderService/OrderService.Infrastructure/ OrderService.Infrastructure/
COPY src/OrderService/OrderService.Domain/ OrderService.Domain/

# COPY entire building-blocks
COPY building-blocks/ building-blocks/

# Restore
RUN dotnet restore "OrderService.API/OrderService.API.csproj"

COPY src/OrderService/ ./OrderService/

# Publish
WORKDIR /src/OrderService/OrderService.API
RUN dotnet publish "OrderService.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5001
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
