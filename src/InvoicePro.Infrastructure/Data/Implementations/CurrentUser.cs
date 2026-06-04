using System.Net;
using System.Security.Claims;
using InvoicePro.Application.Exceptions;
using InvoicePro.Application.interfaces;
using InvoicePro.Domain.Enums;
using Microsoft.AspNetCore.Http;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var value = GetClaimValue(ClaimTypes.NameIdentifier);
            return value is null ? Guid.Empty : Guid.Parse(value);
        }
    }

    public string Email
    {
        get
        {
            return GetClaimValue(ClaimTypes.Email) ?? string.Empty;
        }
    }

    public UserRole Role
    {
        get
        {
            var value = GetClaimValue(ClaimTypes.Role);

            return value is null
                ? throw new AppException(
        "Authentication token is missing or invalid.",
       HttpStatusCode.Unauthorized)
                : Enum.Parse<UserRole>(value);
        }
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(claimType)?.Value ?? throw new AppException(
            $"Missing claim: {claimType}",
            HttpStatusCode.Unauthorized);
    }
}