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
![Docker Containers](images/docker-containers.png)

### RabbitMQ Infrastructure

#### Exchanges
![RabbitMQ Exchange](images/rabbitmq-exchange.png)
#### Queues
![RabbitMQ Queues](images/rabbitmq-queues.png)
#### Bindings
![RabbitMQ Exchange Binding](images/rabbitmq-exchangeBinding.png)

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
