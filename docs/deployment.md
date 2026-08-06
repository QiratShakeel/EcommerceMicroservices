## Running the Project

### Prerequisites

Docker Desktop
.NET SDK
PowerShell

### Configuration Setup
Each service contains an appsettings.example.json file.
#### How to use it:
1. Copy the file:
```text
appsettings.example.json → appsettings.json
```
2. Update only required values:
- ConnectionStrings → SQL Server name / username / password
- RabbitMQ → username / password / host
- JWT → secret key (if needed)
3. Keep other values same as example file.

### Start the Entire System

The Docker Compose files are located in:
deployments/docker/

A helper PowerShell script is provided in the project root:
start.ps1

From the project root directory, run:
.\start.ps1
The script automatically starts all required containers and services.

This includes:

    API Gateway
    Identity Service
    Catalog Service
    Order Service
    Payment Service
    SQL Server
    RabbitMQ



