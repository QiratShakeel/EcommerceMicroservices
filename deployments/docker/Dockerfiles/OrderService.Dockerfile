FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR EcommerceMicroservices/

# Copy project folders
# COPY src/OrderService/OrderService.API/ src/OrderService/OrderService.API/
# COPY src/OrderService/OrderService.Application/ src/OrderService/OrderService.Application/
# COPY src/OrderService/OrderService.Infrastructure/ src/OrderService/OrderService.Infrastructure/
# COPY src/OrderService/OrderService.Domain/ src/OrderService/OrderService.Domain/

# COPY entire building-blocks
COPY src/OrderService/ src/OrderService/
COPY building-blocks/ building-blocks/

# Restore
RUN dotnet restore "src/OrderService/OrderService.API/OrderService.API.csproj"


# Publish
# WORKDIR EcommerceMicroservices/src/OrderService/OrderService.API/
RUN dotnet publish "src/OrderService/OrderService.API/OrderService.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5001
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
