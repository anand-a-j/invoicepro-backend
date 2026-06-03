


InvoicePro/
│
├── InvoicePro.sln
│
├── src/
│   ├── InvoicePro.API/                          # Presentation Layer
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── OrganizationsController.cs
│   │   │   ├── CustomersController.cs
│   │   │   ├── InvoicesController.cs
│   │   │   ├── PaymentsController.cs
│   │   │   └── ReportsController.cs
│   │   ├── Middlewares/
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   └── TenantMiddleware.cs
│   │   ├── Filters/
│   │   │   └── ValidationFilter.cs
│   │   ├── Extensions/
│   │   │   ├── ServiceExtensions.cs
│   │   │   └── SwaggerExtensions.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json
│   │
│   ├── InvoicePro.Application/                  # Business Logic Layer
│   │   ├── DTOs/
│   │   │   ├── Auth/
│   │   │   │   ├── LoginRequestDto.cs
│   │   │   │   ├── RegisterRequestDto.cs
│   │   │   │   └── AuthResponseDto.cs
│   │   │   ├── Organizations/
│   │   │   │   ├── OrganizationDto.cs
│   │   │   │   └── CreateOrganizationDto.csp
│   │   │   ├── Customers/
│   │   │   │   ├── CustomerDto.cs
│   │   │   │   ├── CreateCustomerDto.cs
│   │   │   │   └── UpdateCustomerDto.cs
│   │   │   ├── Services/
│   │   │   │   ├── ServiceDto.cs
│   │   │   │   ├── CreateServiceDto.cs
│   │   │   │   └── UpdateServiceDto.cs
│   │   │   ├── Invoices/
│   │   │   │   ├── InvoiceDto.cs
│   │   │   │   ├── CreateInvoiceDto.cs
│   │   │   │   ├── UpdateInvoiceDto.cs
│   │   │   │   ├── InvoiceLineItemDto.cs
│   │   │   │   └── InvoiceSummaryDto.cs
│   │   │   ├── Payments/
│   │   │   │   ├── PaymentDto.cs
│   │   │   │   └── CreatePaymentDto.cs
│   │   │   └── Reports/
│   │   │       └── RevenueReportDto.cs
│   │   ├── Services/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IAuthService.cs
│   │   │   │   ├── IOrganizationService.cs
│   │   │   │   ├── ICustomerService.cs
│   │   │   │   ├── IServiceCatalogService.cs
│   │   │   │   ├── IInvoiceService.cs
│   │   │   │   ├── IPaymentService.cs
│   │   │   │   └── IReportService.cs
│   │   │   └── Implementations/
│   │   │       ├── AuthService.cs
│   │   │       ├── OrganizationService.cs
│   │   │       ├── CustomerService.cs
│   │   │       ├── ServiceCatalogService.cs
│   │   │       ├── InvoiceService.cs
│   │   │       ├── PaymentService.cs
│   │   │       └── ReportService.cs
│   │   ├── Validators/
│   │   │   ├── CreateInvoiceValidator.cs
│   │   │   ├── CreateCustomerValidator.cs
│   │   │   └── CreatePaymentValidator.cs
│   │   ├── Mappings/
│   │   │   └── MappingProfile.cs
│   │   └── Exceptions/
│   │       ├── BusinessException.cs
│   │       ├── NotFoundException.cs
│   │       └── ValidationException.cs
│   │
│   ├── InvoicePro.Domain/                       # Core Domain Layer
│   │   ├── Entities/
│   │   │   ├── User.cs
│   │   │   ├── Organization.cs
│   │   │   ├── Customer.cs
│   │   │   ├── Service.cs
│   │   │   ├── Invoice.cs
│   │   │   ├── InvoiceLineItem.cs
│   │   │   ├── Payment.cs
│   │   │   └── BaseEntity.cs
│   │   ├── Enums/
│   │   │   ├── InvoiceStatus.cs
│   │   │   ├── PaymentMethod.cs
│   │   │   └── RateType.cs
│   │   └── Constants/
│   │       └── BusinessRules.cs
│   └── InvoicePro.Infrastructure/               # Data Access Layer
│       ├── Data/
│       │   ├── ApplicationDbContext.cs
│       │   └── Configurations/
│       │       ├── UserConfiguration.cs
│       │       ├── OrganizationConfiguration.cs
│       │       ├── CustomerConfiguration.cs
│       │       ├── ServiceConfiguration.cs
│       │       ├── InvoiceConfiguration.cs
│       │       ├── InvoiceLineItemConfiguration.cs
│       │       └── PaymentConfiguration.cs
│       ├── Repositories/
│       │   ├── Interfaces/
│       │   │   ├── IRepository.cs
│       │   │   ├── IUserRepository.cs
│       │   │   ├── IOrganizationRepository.cs
│       │   │   ├── ICustomerRepository.cs
│       │   │   ├── IServiceRepository.cs
│       │   │   ├── IInvoiceRepository.cs
│       │   │   └── IPaymentRepository.cs
│       │   └── Implementations/
│       │       ├── Repository.cs
│       │       ├── UserRepository.cs
│       │       ├── OrganizationRepository.cs
│       │       ├── CustomerRepository.cs
│       │       ├── ServiceRepository.cs
│       │       ├── InvoiceRepository.cs
│       │       └── PaymentRepository.cs
│       ├── Identity/
│       │   ├── JwtTokenGenerator.cs
│       │   └── PasswordHasher.cs
│       └── Migrations/
├── .gitignore
├── README.md
├── Dockerfile                                    # Optional
└── docker-compose.yml                            # Optional