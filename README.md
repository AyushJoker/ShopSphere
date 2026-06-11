# ShopSphere

Enterprise-grade microservices-based e-commerce backend platform built with .NET 9, Clean Architecture, Docker, Redis, and API Gateway patterns.

---

# Overview

ShopSphere is a distributed e-commerce backend platform designed to simulate real-world enterprise architecture and backend engineering practices.

The project focuses on:

* Microservices Architecture
* Clean Architecture
* API Gateway Pattern
* JWT Authentication & Authorization
* Redis Caching
* Service-to-Service Communication
* Dockerized Deployment
* Distributed System Design
* Scalability & Maintainability

---

# System Architecture

```text
Client
   │
   ▼
API Gateway (YARP)
   │
   ├── Identity Service
   ├── Product Service
   ├── Order Service
   └── Inventory Service

Redis
│
├── Identity Service Cache
└── Product Service Cache

SQL Server
│
├── Identity Database
├── Product Database
├── Order Database
└── Inventory Database
```

---

# Current Services

## Identity Service

Responsible for:

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
* Serilog Logging

---

## Product Service

Responsible for:

* Product CRUD
* Category Management
* Pagination
* Searching
* Filtering
* Dynamic Sorting
* Redis Caching
* Cache Invalidation
* FluentValidation
* JWT Authentication
* Role-based Authorization
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

---

## Order Service

Responsible for:

* Order Creation
* Order Retrieval
* User Order History
* Order Cancellation
* Order Number Generation
* Order Status Management
* Product Validation Before Order Creation
* Product Service Integration
* Service-to-Service Communication
* FluentValidation
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

---

## Inventory Service

Responsible for:

* Inventory Tracking
* Add Stock
* Get Inventory By Product
* Inventory Management Foundation
* FluentValidation
* JWT Authentication
* Global Exception Handling
* Structured Error Responses
* Serilog Logging

---

## API Gateway

Built using YARP Reverse Proxy.

Responsibilities:

* Unified Entry Point
* Reverse Proxy Routing
* Identity Service Routing
* Product Service Routing
* Order Service Routing
* Inventory Service Routing
* Gateway Rate Limiting
* Service Forwarding
* Future Cross-Cutting Concerns

---

# Tech Stack

## Backend

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* FluentValidation

## Database

* SQL Server
* Redis

## Security

* JWT Authentication
* JWT Authorization
* BCrypt Password Hashing
* Refresh Tokens
* Refresh Token Rotation

## Infrastructure

* Docker
* Docker Compose
* YARP API Gateway

## Logging

* Serilog
* Request Logging
* Structured Logging

---

# Architecture Patterns

The project follows:

* Microservices Architecture
* Clean Architecture
* Repository Pattern
* Dependency Injection
* Service-to-Service Communication
* API Gateway Pattern
* Cache-Aside Pattern
* Global Exception Handling Pattern

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
│   │
│   ├── ProductService
│   │
│   ├── OrderService
│   │
│   ├── InventoryService
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

## Start Services

```bash
cd docker

docker compose up --build
```

## Stop Services

```bash
docker compose down
```

---

# Service URLs

## Identity Service

```text
http://localhost:5000/swagger/index.html
```

## Product Service

```text
http://localhost:5001/swagger/index.html
```

## Order Service

```text
http://localhost:5002/swagger/index.html
```

## Inventory Service

```text
http://localhost:5003/swagger/index.html
```

---

# Gateway URLs

## Identity

```text
http://localhost:7000/identity/swagger/index.html
```

## Products

```text
http://localhost:7000/products/swagger/index.html
```

## Orders

```text
http://localhost:7000/orders/swagger/index.html
```

## Inventory

```text
http://localhost:7000/inventory/swagger/index.html
```

---

# Implemented Features

## Authentication & Security

* JWT Authentication
* Refresh Tokens
* Refresh Token Rotation
* Role-based Authorization
* BCrypt Password Hashing
* API Rate Limiting

## Caching

* Redis Integration
* User Profile Caching
* Product List Caching
* Cache Invalidation Strategies

## Validation

* FluentValidation
* Request Validation

## Logging & Monitoring

* Serilog Logging
* Request Logging
* Global Exception Handling
* Structured Error Responses

## Service Communication

* Order → Product Service Communication
* HttpClient Integration
* Product Validation Before Order Creation

## Infrastructure

* Dockerized Services
* Docker Compose Orchestration
* API Gateway Routing
* Redis Integration

---

# Upcoming Features

## Inventory Management

* Stock Reservation
* Stock Release
* Stock Deduction
* Inventory Reservation Workflow
* Low Stock Alerts

## Event-Driven Architecture

* RabbitMQ Integration
* Event Publishing
* Event Consumption
* Inventory Events
* Order Events

## Observability

* OpenTelemetry
* Prometheus
* Grafana
* Distributed Tracing

## DevOps

* GitHub Actions CI/CD
* Container Registry Publishing
* Kubernetes Deployment
* Helm Charts

## Future Services

* Cart Service
* Payment Service
* Notification Service
* Review Service

## Frontend

* Angular Frontend
* Authentication Flow
* Product Management UI
* Inventory Management UI
* Cart UI
* Order Management UI

---

# Learning Goals

This project is focused on implementing real-world backend engineering concepts including:

* Enterprise Backend Architecture
* Scalable Microservices
* Distributed Systems
* Cloud-Native Development
* API Gateway Design
* Service-to-Service Communication
* Caching Strategies
* Production-Ready Backend Practices
