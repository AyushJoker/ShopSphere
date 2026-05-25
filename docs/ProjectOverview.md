# ShopSphere

## Overview
ShopSphere is a microservices-based e-commerce backend platform built using .NET 9 and Clean Architecture principles.

The goal of this project is to simulate enterprise-grade backend development practices including:
- Microservices architecture
- API Gateway
- JWT Authentication
- Refresh Tokens
- Redis Caching
- Docker containerization
- Clean Architecture
- Centralized logging
- Rate limiting
- Scalable infrastructure

---

## Current Services

### Identity Service
Handles:
- User Registration
- User Login
- JWT Authentication
- Refresh Tokens
- Role Management
- User Profile APIs
- Redis Caching

### API Gateway
Built using YARP Reverse Proxy.
Responsible for:
- Request routing
- Service aggregation
- Entry point for clients

---

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Redis
- Docker
- YARP API Gateway
- Serilog
- JWT Authentication

---

## Architecture Style

- Microservices Architecture
- Clean Architecture
- Repository Pattern
- Dependency Injection

---

## Current Status

Completed:
- Identity Service
- JWT Authentication
- Refresh Tokens
- Redis Caching
- Dockerization
- API Gateway Integration

In Progress:
- Product Service
- Event-driven communication
- Observability
- Kubernetes deployment