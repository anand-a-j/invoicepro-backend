using InvoicePro.Domain.Entities;

namespace InvoicePro.Application.Interfaces.Identity;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}