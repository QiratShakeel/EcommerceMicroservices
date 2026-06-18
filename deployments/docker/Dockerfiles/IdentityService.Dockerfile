FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR EcommerceMicroservices/

# COPY building-blocks/ building-blocks/
# COPY src/IdentityService/ src/IdentityService/
COPY . .
# Restore only API project
RUN dotnet restore "src/IdentityService/Identity.API/Identity.API.csproj"
# Publish only API project
RUN dotnet publish "src/IdentityService/Identity.API/Identity.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5003 5005
ENTRYPOINT ["dotnet","Identity.API.dll"]
