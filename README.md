# InvoicePro - Invoice Management API

InvoicePro is a RESTful Invoice Management API built with ASP.NET Core, PostgreSQL, and Clean Architecture principles, featuring JWT authentication and customer, invoice, and organization management. It demonstrates modern .NET backend development practices including Repository Pattern, Dependency Injection, Entity Framework Core, and scalable layered architecture.

flowchart LR

    Client["👨‍💻 Client App"]

    Client --> API["🌐 ASP.NET Core Web API"]

    API --> Middleware["⚠️ Exception Middleware"]

    Middleware --> Controllers["📦 Controllers
    Auth
    Customer
    Invoice
    Organization"]

    Controllers --> Services["📋 Application Services
    AuthService
    CustomerService
    InvoiceService
    OrganizationService"]

    Services --> Domain["🏛️ Domain Layer
    User
    Customer
    Invoice
    InvoiceItem
    Organization"]

    Services --> Repositories["📂 Repositories
    UserRepository
    CustomerRepository
    InvoiceRepository
    OrganizationRepository"]

    Services --> JWT["🔐 JWT Token Generator"]
    Services --> Hasher["🔑 Password Hasher"]

    Repositories --> EF["⚙️ Entity Framework Core"]

    EF --> DB[("🐘 PostgreSQL")]

    classDef primary fill:#2563eb,color:white
    classDef secondary fill:#059669,color:white
    classDef security fill:#dc2626,color:white
    classDef db fill:#7c3aed,color:white

    class API,Middleware,Controllers,Services primary
    class Domain,Repositories,EF secondary
    class JWT,Hasher security
    class DB db


## Technical Highlights
- Clean Architecture principles
- ASP.NET Core Web API (.NET 9)
- JWT Authentication & Authorization
- Repository Pattern
- Service Layer Architecture
- Dependency Injection
- Entity Framework Core
- PostgreSQL Integration
- EF Core Entity Relationships
- Password Hashing & Security
- Global Exception Handling Middleware
- Standardized API Response Structure
- Swagger / OpenAPI Documentation
- RESTful API Design 