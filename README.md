# Microservices E-Commerce Backend
![.NET](https://img.shields.io/badge/.NET-9-blue)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Enabled-orange)
![Docker](https://img.shields.io/badge/Docker-Compose-blue)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-red)
## Status
✅ Completed

## Overview
This project is a production-inspired E-Commerce backend built with ASP.NET Core using a microservices architecture. It demonstrates how independent services communicate through synchronous (gRPC) and asynchronous (RabbitMQ) messaging while following modern backend design principles such as Clean Architecture, Domain-Driven Design (DDD), CQRS, and the Transactional Outbox Pattern. The solution includes an API Gateway, JWT/OIDC authentication, Dockerized deployment, and automated testing, providing a practical example of building scalable, maintainable, and event-driven distributed systems.

![Architecture](docs/images/architecture.png)

```md
## Demo

![Demo](docs/images/demo.gif)
```
## Features
Architecture
- Microservices-based E-Commerce Architecture
- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS with MediatR
- Repository Pattern

ASP.NET
- Entity Framework Core with SQL Server
- FluentValidation

Security
- OpenID Connect (OIDC)
- JWT Authentication

Communication
- gRPC Communication
- RabbitMQ Messaging

System Design
- Transactional Outbox Pattern
- At-Least-Once Delivery
- Inventory Synchronization
- API Gateway using YARP

Deployment
- Dockerized Deployment
- Health Checks

Testing
- Unit & Integration Testing

## Technology Stack

- ASP.NET Core Web API
- C#
- SQL Server
- Entity Framework Core
- MediatR
- FluentValidation
- RabbitMQ
- gRPC
- YARP API Gateway
- Docker
- xUnit
- Git & GitHub

## Architecture Components

### Identity Service

User Registration
![Register User](docs/images/register.png)
User Login
![Login User](docs/images/login.png)
User Queries    
- OIDC Authentication    
- JWT Token Issuance

### Catalog Service

#### Product Management
Create Product
Update Product
Delete Product
Inventory Management

#### Category Management
Create Category
Update Category
Delete Category
    
#### Product Queries
Get Products 
Get Product By Id

#### Category Queries
Get Categories
Get Category By Id

>     Note: Inventory synchronization is performed through event-driven communication.
> 
>     Inventory is reduced only after OrderCompleted event, ensuring payment success before stock deduction.
> 
>     This prevents stock reduction for failed or cancelled orders and maintains inventory consistency.

### Order Service

Order Creation
![Create Order](docs/images/create-order.png)
Order Queries
- Order Status Tracking


## Business Workflow
````text
1. User sends requests through API Gateway
2. User registers through Identity Service
3. User logs in and receives an access token
4. Order is created through Order Service
5. Payment Service consumes OrderCreated event
6. Payment Service processes payment
7. PaymentSucceeded or PaymentFailed event is published
8. Order Service consumes payment result event
9. Order status changes to Completed or Cancelled
10. If completed, OrderCompleted event is published
11. Catalog Service updates inventory
````
>  Note: All client requests are routed through the API Gateway.

### Event Flow

```text
Order Service
    ↓
RabbitMQ Exchange
    ↓
Payment Service

Payment Service
    ↓
RabbitMQ Exchange
    ↓
Order Service

Order Service
    ↓
RabbitMQ Exchange
    ↓
Catalog Service

```
### Events:

| Event            | Publisher       | Consumer        | Purpose          |
| ---------------- | --------------- | --------------- | ---------------- |
| OrderCreated     | Order Service   | Payment Service | Initiate Payment |
| PaymentSucceeded | Payment Service | Order Service   | Complete Order   |
| PaymentFailed    | Payment Service | Order Service   | Cancel Order     |
| OrderCompleted   | Order Service   | Catalog Service | Reduce Inventory |


## Project Structure

```text
src/
building-blocks/
api-gateway/
deployments/
docs/
```

## Testing Strategy
### Unit Tests
- Domain Layer
- Application Layer
- CQRS Handlers
- Validation Rules
    
### Integration Tests
- EF Core Persistence
- Repository Layer

## Running the Project

### Prerequisites

Docker Desktop
.NET SDK
PowerShell

Run

From the project root directory, run:
.\start.ps1


## API Documentation

Postman collection is available in:
docs/postman/

Execute requests in this order:

1. Register User
2. Login User
3. Get User By Id
5. Create Category
6. Create Product
7. Create Order

> For detailed API testing instructions, see docs/api-testing.md

## Future Enhancements
- Consumer Idempotency
- Inbox Pattern
- Correlation IDs
- Centralized Logging with ELK / Seq
- Redis Distributed Caching
- Kubernetes Deployment
- CI/CD using GitHub Actions


## Author
Qirat Shakeel
ASP.NET Core Backend Developer interested in distributed systems, microservices and scalable backend architectures.


## Project Links
- 🔗 Repository: https://github.com/QiratShakeel/EcommerceMicroservices
- 🔗 GitHub Profile: https://github.com/QiratShakeel