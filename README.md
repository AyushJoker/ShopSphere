# ShopSphere

Enterprise-grade microservices-based e-commerce backend platform built using .NET 9 and Clean Architecture principles.

---

# Overview

ShopSphere is designed to simulate real-world enterprise backend architecture using:
- Microservices
- API Gateway
- JWT Authentication
- Redis Caching
- Docker
- Clean Architecture
- Distributed system concepts

The project focuses heavily on:
- scalability
- maintainability
- security
- enterprise architecture practices

---

# Current Services

## Identity Service

Handles:
- User Registration
- User Login
- JWT Authentication
- Refresh Tokens
- Role-based Authorization
- User Profile APIs
- Redis Caching

---

## API Gateway

Built using YARP Reverse Proxy.

Responsibilities:
- Reverse proxy routing
- Unified entry point
- Service forwarding
- Future centralized middleware

---

# Tech Stack

## Backend
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core

## Database
- SQL Server
- Redis Cache

## Security
- JWT Authentication
- BCrypt Password Hashing
- Refresh Tokens

## Infrastructure
- Docker
- Docker Compose
- YARP API Gateway

## Logging
- Serilog

---

# Architecture

The project follows:
- Microservices Architecture
- Clean Architecture
- Repository Pattern
- Dependency Injection

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
git clone <your-repository-url>
```

---

## Start Docker Containers

Navigate to:

```bash
cd docker
```

Run:

```bash
docker compose up --build
```

---

# Service URLs

## Identity Service Swagger

```text
http://localhost:5000/swagger/index.html
```

---

## Gateway Swagger

```text
http://localhost:7000/identity/swagger/index.html
```

---

# Current Features

- JWT Authentication
- Refresh Tokens
- Redis Caching
- Dockerized Services
- API Gateway
- Global Exception Handling
- Request Logging
- Role-based Authorization

---

# Upcoming Features

- Product Service
- Order Service
- RabbitMQ
- OpenTelemetry
- Grafana
- Kubernetes
- CI/CD Pipelines
- React Frontend

---

# Learning Goals

This project is focused on learning and implementing:
- Enterprise backend architecture
- Scalable microservices
- Distributed systems
- Cloud-native development
- Production-grade backend practices