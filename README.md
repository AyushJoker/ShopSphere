# ShopSphere

Enterprise-grade microservices-based e-commerce backend platform built using .NET 9 and Clean Architecture principles.

---

# Overview

ShopSphere is designed to simulate real-world enterprise backend architecture using:

* Microservices
* API Gateway
* JWT Authentication
* Redis Caching
* Docker
* Clean Architecture
* Service-to-Service Communication
* Distributed System Concepts

The project focuses heavily on:

* Scalability
* Maintainability
* Security
* Enterprise Architecture Practices

---

# Current Services

## Identity Service

Handles:

* User Registration
* User Login
* JWT Authentication
* Refresh Tokens
* Refresh Token Rotation
* Role-based Authorization
* User Profile APIs
* Redis Caching
* API Rate Limiting
* Global Exception Handling
* Structured Error Responses
* Serilog Request Logging

---

## Product Service

Handles:

* Product CRUD
* Category Management
* Pagination
* Filtering
* Searching
* Dynamic Sorting
* FluentValidation
* Redis Caching
* Cache Invalidation
* JWT Authentication
* Role-based Authorization
* Global Exception Handling
* Structured Error Responses
* Serilog Request Logging

---

## Order Service

Handles:

* Order Creation
* Get Order By Id
* Get User Orders
* Order Cancellation
* Order Number Generation
* Order Status Management
* Product Validation Before Order Creation
* Service-to-Service Communication
* FluentValidation
* Global Exception Handling
* Structured Error Responses
* Serilog Request Logging

---

## API Gateway

Built using YARP Reverse Proxy.

Responsibilities:

* Reverse Proxy Routing
* Unified Entry Point
* Service Forwarding
* Gateway Rate Limiting
* Future Centralized Middleware

---

# Tech Stack

## Backend

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* FluentValidation

## Database

* SQL Server
* Redis Cache

## Security

* JWT Authentication
* BCrypt Password Hashing
* Refresh Tokens

### Product Service Authorization

| Endpoint          | Access    |
| ----------------- | --------- |
| GET Products      | Anonymous |
| GET Product By Id | Anonymous |
| Create Product    | Admin     |
| Update Product    | Admin     |
| Delete Product    | Admin     |

### Order Service Authorization

| Endpoint        | Access             |
| --------------- | ------------------ |
| Create Order    | Authenticated User |
| Get My Orders   | Authenticated User |
| Get Order By Id | Authenticated User |
| Cancel Order    | Authenticated User |

## Infrastructure

* Docker
* Docker Compose
* YARP API Gateway

## Logging

* Serilog
* Request Logging
* File Logging

---

# Architecture

The project emphasizes production-style backend development patterns and scalable service separation.

The project follows:

* Microservices Architecture
* Clean Architecture
* Repository Pattern
* Dependency Injection
* Service-to-Service Communication

---

# Repository Structure

```text
ShopSphere
│
├── ShopSphere.AllServices.sln
│
├── services
│   │
│   ├── IdentityService
│   │   ├── ShopSphere.IdentityService.sln
│   │   ├── ShopSphere.IdentityService.API
│   │   ├── ShopSphere.IdentityService.Application
│   │   ├── ShopSphere.IdentityService.Domain
│   │   └── ShopSphere.IdentityService.Infrastructure
│   │
│   ├── ProductService
│   │   ├── ShopSphere.ProductService.sln
│   │   ├── ShopSphere.ProductService.API
│   │   ├── ShopSphere.ProductService.Application
│   │   ├── ShopSphere.ProductService.Domain
│   │   └── ShopSphere.ProductService.Infrastructure
│   │
│   ├── OrderService
│   │   ├── ShopSphere.OrderService.sln
│   │   ├── ShopSphere.OrderService.API
│   │   ├── ShopSphere.OrderService.Application
│   │   ├── ShopSphere.OrderService.Domain
│   │   └── ShopSphere.OrderService.Infrastructure
│   │
│   └── ShopSphere.ApiGateway
│
├── docker
├── docs
├── frontend
├── architecture
└── .github
```

---

# Running the Project

## Clone Repository

```bash
git clone https://github.com/AyushJoker/ShopSphere.git
```

---

## Start Docker Containers

```bash
cd docker
docker compose up --build
```

---

# Service URLs

## Identity Service Swagger

```text
http://localhost:5000/swagger/index.html
```

## Product Service Swagger

```text
http://localhost:5001/swagger/index.html
```

## Order Service Swagger

```text
http://localhost:5002/swagger/index.html
```

## Gateway Identity Swagger

```text
http://localhost:7000/identity/swagger/index.html
```

## Gateway Product Swagger

```text
http://localhost:7000/products/swagger/index.html
```

---

# Current Features

## Identity Service

* JWT Authentication
* Refresh Tokens
* Refresh Token Rotation
* Role-based Authorization
* Redis Caching
* API Rate Limiting
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

## Product Service

* Product CRUD
* Category Management
* Pagination
* Searching
* Filtering
* Dynamic Sorting
* FluentValidation
* Redis Caching
* Cache Invalidation
* JWT Authentication
* Role-based Authorization
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

## Order Service

* Order Creation
* Order Retrieval
* User Order History
* Order Cancellation
* Order Number Generation
* FluentValidation
* Product Service Integration
* Service-to-Service Communication
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

## API Gateway

* YARP Reverse Proxy
* Identity Service Routing
* Product Service Routing
* Gateway Rate Limiting
* Swagger Access Through Gateway

## Platform

* Dockerized Services
* Docker Compose
* Clean Architecture
* Repository Pattern
* Dependency Injection
* Service-to-Service Communication

---

# Upcoming Features

## Order Service

* Gateway Integration
* Partial Shipment Support
* Order Line Status Tracking

## Inventory Service

* Stock Management
* Inventory Reservation
* Low Stock Alerts
* Inventory Updates

## Event-Driven Communication

* RabbitMQ
* Event Publishing
* Event Consumption

## Observability

* OpenTelemetry
* Prometheus
* Grafana

## DevOps

* GitHub Actions CI/CD
* Kubernetes
* Helm Charts

## Frontend

* Angular Frontend
* Authentication Flow
* Product UI
* Cart UI
* Order Management UI

---

# Learning Goals

This project is focused on learning and implementing:

* Enterprise Backend Architecture
* Scalable Microservices
* Distributed Systems
* Cloud-native Development
* Production-grade Backend Practices
