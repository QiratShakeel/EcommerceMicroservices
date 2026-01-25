Microservices eCommerce Backend (Catalog Service)

This is a personal learning project focused on building a scalable backend
using ASP.NET Core and microservices architecture.  
Currently, the project includes the Catalog microservice, designed following
Clean Architecture and DDD principles.



Features Implemented

 Catalog microservice built with ASP.NET Core Web API
 Clean Architecture (Domain, Application, Infrastructure, API)
 CQRS pattern using MediatR
 Request validation using FluentValidation
 Entity Framework Core with Fluent API configurations
 SQL Server integration
 DomainDriven Design concepts:
   Aggregates
   Entities
   Value Objects
   Domain Events
 Basic eventdriven approach using domain events
 Repository pattern
 Unit & Integration testing using xUnit



Tech Stack

 Backend: ASP.NET Core Web API
 Language: C#
 Database: SQL Server
 ORM: Entity Framework Core
 Architecture: Clean Architecture, CQRS
 Messaging: Domain Events (basic)
 Validation: FluentValidation
 Testing: xUnit
 Tools: Git, GitHub, Postman, Docker (basic)


Architecture Overview
The solution follows Clean Architecture:

 Domain – Core business logic (Entities, Aggregates, Domain Events)
 Application – CQRS handlers, validation, business rules
 Infrastructure – EF Core, repositories, database configuration
 API – Controllers and HTTP endpoints

Each layer depends only on inner layers.

How to Run

1. Clone the repository
2. Create `appsettings.json` from `appsettings.example.json`
3. Update SQL Server connection string
4. Apply migrations:
   ```bash
   dotnet ef database update
5. Run the API:
    dotnet run

Swagger UI will be available at:

CatalogService:
    http://localhost:5108/swagger/index.html
OrderService:
    http://localhost:5278/swagger/index.html

Testing: 

 Unit and integration tests written using xUnit

Tests cover:

 Domain logic
 Application handlers
 EF Core persistence

Project Status:

 In Progress

Planned improvements:

 Additional microservices (Ordering, Basket)
 Message broker integration
 API Gateway
 Improved event-driven communication

Author

 Qirat Shakeel
 Junior .NET Backend Developer