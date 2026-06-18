# Microservices E-Commerce Backend
![.NET](https://img.shields.io/badge/.NET-9-blue)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Enabled-orange)
![Docker](https://img.shields.io/badge/Docker-Compose-blue)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-red)
## Status
✅ Completed

This project demonstrates a production-grade distributed microservices system.

### Implemented

- Identity Service
- Catalog Service
- Order Service
- Payment Service
- API Gateway
- RabbitMQ Messaging
- gRPC Communication
- Transactional Outbox Pattern
- Docker Deployment

![Architecture](docs/images/architecture.png)

## Technology Overview
```text
Client
   │
   ▼
API Gateway (YARP)
   │
   ├── Identity Service
   ├── Catalog Service
   ├── Order Service
   └── Payment Service

Infrastructure
├── SQL Server
├── RabbitMQ
└── gRPC
```
## Features
- Microservices-based E-Commerce Architecture
- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS with MediatR
- Entity Framework Core with SQL Server
- Repository Pattern
- FluentValidation
- OpenID Connect (OIDC)
- OpenIddict Authorization Server
- JWT Authentication
- API Gateway using YARP
- gRPC Communication
- RabbitMQ Messaging
- Transactional Outbox Pattern
- At-Least-Once Delivery
- Inventory Synchronization
- Dockerized Deployment
- Health Checks
- Unit & Integration Testing

## Architecture Highlights
- Clean Architecture per service
- DDD aggregates and domain events
- CQRS with MediatR
- Event-driven communication with RabbitMQ
- Transactional Outbox Pattern
- API Gateway using YARP
- gRPC service-to-service communication

## System Design Overview
This project implements a production-level microservices architecture with:

- Event-Driven Architecture (RabbitMQ)
- Synchronous Communication (gRPC)
- API Gateway Pattern (YARP)
- Database per Service
- CQRS + Clean Architecture per microservice
- Transactional Outbox Pattern for reliability
- Eventually consistent distributed system design

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

## Architecture Components

### Identity Service

User Registration
![Register User](docs/images/register.png)
User Login
![Login User](docs/images/login.png)
User Queries    
![Get Users](docs/images/get-users.png)
![Get User By Id](docs/images/get-userById.png)
- OIDC Authentication    
- JWT Token Issuance

### Catalog Service

#### Product Management
Create Product
![Create Product](docs/images/create-product.png)
Update Product
![Update Product](docs/images/update-product.png)
Delete Product
![Delete Product](docs/images/delete-product.png)

#### Category Management
Create Category
![Create Category](docs/images/create-category.png)
Update Category
![Update Category](docs/images/update-category.png)
Delete Category
![Delete Category](docs/images/delete-category.png)
    
Inventory Management
![Inventory Managment](docs/images/reduced-stockAfterOrder.png)
    
#### Product Queries
Get Products 
![Get Products](docs/images/get-products.png)
Get Product By Id
![Get Product By Id](docs/images/get-productById.png)

#### Category Queries
Get Categories
![Get Categories](docs/images/get-categories.png)
Get Category By Id
![Get Category By Id](docs/images/get-categoryById.png)

>     Note: Inventory synchronization is performed through event-driven communication.
> 
>     Inventory is reduced only after OrderCompleted event, ensuring payment success before stock deduction.
> 
>     This prevents stock reduction for failed or cancelled orders and maintains inventory consistency.

### Order Service

Order Creation
![Create Order](docs/images/create-order.png)
Order Queries
![Get Order By Id](docs/images/get-orderById.png)
- Order Status Tracking

#### Order Status Flow
```text
Order Created
    ↓
Order + Outbox Event stored in same transaction
    ↓
Outbox Publisher sends event to RabbitMQ
    ↓
Payment Service consumes OrderCreated
    ↓
Payment Processing
    ↓
PaymentSucceeded Event Received
    ↓
Order Status → Completed

PaymentFailed Event Received
    ↓
Order Status → Cancelled
```

### Payment Service

- Payment Processing
- Success/Failure Events
> Note: Payment processing is performed asynchronously after order creation; therefore, no public Payment API is exposed.

### API Gateway
- Centralized Routing using YARP
- Authentication Enforcement
- Service Aggregation
    
### Docker Infrastructure
![Docker Containers](docs/images/docker-containers.png)

### RabbitMQ Infrastructure

#### Exchanges
![RabbitMQ Exchange](docs/images/rabbitmq-exchange.png)
#### Queues
![RabbitMQ Queues](docs/images/rabbitmq-queues.png)
#### Bindings
![RabbitMQ Exchange Binding](docs/images/rabbitmq-exchangeBinding.png)

### gRPC Communication
Used for high-performance synchronous service-to-service communication.
During order creation, Order Service retrieves product details from Catalog Service using gRPC.
Benefits:
- High performance
- Contract-first design using Protocol Buffers (Protobuf)
- Strong typing
- Low latency communication between services

### Messaging Architecture
RabbitMQ is used for asynchronous communication between services.
Messaging is used for:
- Payment Processing
- Order Completion
- Inventory Synchronization
    
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
    

### Reliability Patterns
#### Transactional Outbox Pattern
To ensure reliable event delivery, the system implements the Transactional Outbox Pattern.
The pattern is used for:
- OrderCreated
- PaymentSucceeded
- PaymentFailed
- OrderCompleted
A background Outbox Processor publishes pending events to RabbitMQ, ensuring reliable event delivery and eventual consistency across services.
Benefits:
- Prevents lost messages
- Guarantees eventual event publication
- Handles service crashes during event creation
- Improves consistency between database state and message broker state
##### Message Delivery
The system follows an At-Least-Once Delivery model.
Ensures reliable event publishing even in case of service crashes or database failures.
> Duplicate message handling is planned through Consumer Idempotency and Inbox Pattern implementation.

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

## Project Structure

```text
src/
├── CatalogService
├── IdentityService
├── OrderService
└── PaymentService

building-blocks/
├── BuildingBlocks.Infrastructure
├── BuildingBlocks.Shared
├── EventBus.Abstractions
├── EventBus.RabbitMQ
└── Tests

api-gateway/
└── ApiGateway

deployments/
└── docker

docs/
├── images
└── postman
```

## Catalog Service

```text
CatalogService/
├── Catalog.Api
├── Catalog.Application
├── Catalog.Domain
├── Catalog.Infrastructure
└── Tests
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


## API Documentation

### Postman Testing Workflow (Variables System)

![Postman Variables](docs/images/postman-collectionVariables.png)

### Postman Collection:
Postman collection is available in:
docs/postman/

Import the collection into Postman before testing APIs.

### Collection Variables
The collection uses Postman Collection Variables.
Variables are automatically stored during request execution and reused by subsequent requests.

### API Testing Order
#### Identity Service

1. Register User
The register request automatically stores:

- email
- password
- userId
in Collection Variables.

2. Login User
Uses:
    {{email}}
    {{password}}
from Collection Variables.

After successful login, the request automatically stores:
    {{token}}

3. Get User By Id
Uses:
    {{userId}}

4. Get Users

#### Catalog Service
5. Create Category
Automatically stores:
    {{categoryId}}

6. Get Categories

7. Get Category By Id
Uses:
    {{categoryId}}

8. Create Product
Uses:
    {{categoryId}}

Automatically stores:
    {{productId}}


9. Get Products

10. Get Product By Id
Uses:
    {{productId}}

11. Update Category
Uses: 
    {{categoryId}}


12. Update Product
Uses:
    {{productId}}

#### Order Service
13. Create Order
Uses:
    {{productId}}
Automatically stores:
    {{orderId}}

14. Get Order By Id
Uses:
    {{orderId}}

15. Delete Product
Uses:
    {{productId}}
Removes:
    {{productId}}
from Collection Variables.

16. Delete Category
Uses:
    {{categoryId}}
Removes:
    {{categoryId}}
from Collection Variables.

### Collection Variable Dependencies
The collection is designed to be executed sequentially.
Several requests depend on variables generated by previous requests:

    Register User
        ↓
    email
    password
    userId

    Login User
        ↓
    uses email + password
        ↓
    token

    Create Category
        ↓
    categoryId

    Create Product
        ↓
    uses categoryId
        ↓
    productId

    Create Order
        ↓
    uses productId
        ↓
    orderId


```text
Most request URLs and request bodies use Collection Variables such as:

-    {{baseUrl}}
-    {{token}}
-    {{userId}}
-    {{categoryId}}
-    {{productId}}
-    {{orderId}}
-    {{email}}
-    {{password}}
Therefore, requests should be executed in the documented order.
```

> Important:
> 
>     The Postman collection depends on successful execution of previous requests.

> If Order creation fails or asynchronous payment processing has not completed yet,subsequent requests may return unexpected or invalid results.

### For accurate testing:
1. Execute requests in the documented order.
2. Verify Order creation succeeds before proceeding.
3. Wait for payment events to be processed through RabbitMQ before checking final order status.

## Future Enhancements
- Consumer Idempotency
- Inbox Pattern
- Correlation IDs
- OpenTelemetry Distributed Tracing
- Centralized Logging with ELK / Seq
- Redis Distributed Caching
- Kubernetes Deployment
- CI/CD using GitHub Actions
- Saga Pattern for Long Running Transactions
- Dead Letter Queues (DLQ)
- Advanced Retry Policies for gRPC and inter-service communication
- Distributed Rate Limiting
- Application Health Checks (/health)
- Advanced Service Monitoring


> Services, databases, and supporting infrastructure are containerized for local development and deployment.

## Author
Qirat Shakeel
Backend Developer specializing in:
- ASP.NET Core
- Microservices Architecture
- Clean Architecture
- DDD
- CQRS
- RabbitMQ
- SQL Server

## Project Links
- 🔗 Repository: https://github.com/QiratShakeel/EcommerceMicroservices
- 🔗 GitHub Profile: https://github.com/QiratShakeel