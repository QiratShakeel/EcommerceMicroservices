FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR EcommerceMicroservices/


#----------minimal copy 
#COPY building-blocks/ building-blocks/
#COPY src/PaymentService/ src/PaymentService/
##COPY Directory.Packages.props ./
##COPY *.sln ./

#------- .: host ka current . container ka current folder
COPY . .

#RUN ls -R /EcommerceMicroservices/building-blocks

RUN dotnet restore "src/PaymentService/Payment.API/Payment.API.csproj"
RUN dotnet publish "src/PaymentService/Payment.API/Payment.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5002
ENTRYPOINT ["dotnet","Payment.API.dll"]
