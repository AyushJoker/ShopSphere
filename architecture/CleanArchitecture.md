# Clean Architecture

The Identity Service follows Clean Architecture principles to ensure:
- Separation of concerns
- Scalability
- Maintainability
- Testability
- Independent business logic

---

# Architecture Layers

```text
API
 ?
Application
 ?
Domain
 ?
Infrastructure
```

---

# Layer Responsibilities

## Domain Layer

Location:

```text
ShopSphere.IdentityService.Domain
```

Contains:
- Entities
- Enums
- Core business rules

Examples:
- User
- RefreshToken
- UserRole

This layer:
- has NO external dependencies
- is the core of the application
- contains enterprise business models

---

## Application Layer

Location:

```text
ShopSphere.IdentityService.Application
```

Contains:
- DTOs
- Interfaces
- Service contracts
- Business logic
- Application services

Examples:
- IAuthService
- IUserRepository
- AuthService
- RegisterRequestDto

This layer:
- depends only on Domain
- defines abstractions
- contains use-case logic

---

## Infrastructure Layer

Location:

```text
ShopSphere.IdentityService.Infrastructure
```

Contains:
- Database access
- Repository implementations
- JWT implementation
- Redis integration
- External service integrations

Examples:
- UserRepository
- JwtTokenGenerator
- RedisCacheService
- IdentityDbContext

This layer:
- implements Application interfaces
- communicates with external systems

---

## API Layer

Location:

```text
ShopSphere.IdentityService.API
```

Contains:
- Controllers
- Middleware
- Swagger
- Dependency Injection
- Authentication configuration
- Rate limiting configuration

Examples:
- AuthController
- UsersController
- ExceptionMiddleware

This layer:
- acts as the entry point
- handles HTTP communication
- should remain thin

---

# Dependency Rule

Dependencies should flow inward only.

```text
API ? Application ? Domain
Infrastructure ? Application ? Domain
```

Domain should never depend on:
- Infrastructure
- API
- External frameworks

---

# Benefits of Clean Architecture

- Easier testing
- Independent business logic
- Better maintainability
- Easier scalability
- Framework independence
- Cleaner separation of concerns
- Enterprise-grade structure