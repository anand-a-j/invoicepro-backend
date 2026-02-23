using Microsoft.OpenApi.Models;

namespace InvoicePro.API.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
       {
           options.SwaggerDoc("v1", new OpenApiInfo
           {
               Title = "InvoicePro",
               Version = "v1"
           });

           options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
           {
               Name = "Authorization",
               Type = SecuritySchemeType.Http,
               Scheme = "bearer",
               BearerFormat = "JWT",
               In = ParameterLocation.Header,
               Description = "Enter JWT token"
           });

           options.AddSecurityRequirement(new OpenApiSecurityRequirement
           {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
           });
       });

       return services;

    }
}