using InvoicePro.Application.interfaces;
using InvoicePro.Application.Interfaces.Identity;
using InvoicePro.Infrastructure.Identity;
using InvoicePro.Infrastructure.Implementations;
using InvoicePro.Interfaces.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InvoicePro.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}