using InvoicePro.Application.interfaces;
using InvoicePro.Application.Interfaces.Identity;
using InvoicePro.Application.Services;
using InvoicePro.Interfaces.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InvoicePro.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IOrganizationSerivce, OrganizationService>();



        return services;
    }
}