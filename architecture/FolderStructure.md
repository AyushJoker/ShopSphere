# Folder Structure

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

## Folder Responsibilities

### services
Contains all backend microservices and gateway components.

---

### IdentityService
Self-contained authentication and identity microservice.

Contains:
- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

Built using Clean Architecture principles.

---

### ShopSphere.ApiGateway
API Gateway built using YARP reverse proxy.

Responsible for:
- Request routing
- Single entry point
- Service forwarding
- Future centralized middleware

---

### docker
Contains Docker Compose and container orchestration configuration.

---

### docs
General project documentation and development tracking.

---

### architecture
Architecture decisions and system design documentation.

---

### frontend
Frontend applications (future scope).

---

### .github
GitHub workflows and CI/CD pipelines.