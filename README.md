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

## API Gateway

* YARP Reverse Proxy
* Identity Service Routing
* Product Service Routing
* Swagger Access Through Gateway
* Gateway Rate Limiting

## Platform

* Dockerized Services
* Docker Compose
* Clean Architecture
* Repository Pattern
* Dependency Injection

---

# Upcoming Features

* Order Service
* Inventory Service
* RabbitMQ
* OpenTelemetry
* Prometheus
* Grafana
* Kubernetes
* CI/CD Pipelines
* Angular Frontend

---

# Learning Goals

This project is focused on learning and implementing:

* Enterprise Backend Architecture
* Scalable Microservices
* Distributed Systems
* Cloud-native Development
* Production-grade Backend Practices
