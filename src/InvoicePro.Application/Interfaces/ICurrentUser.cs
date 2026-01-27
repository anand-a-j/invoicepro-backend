
using InvoicePro.Domain.Enums;

namespace InvoicePro.Application.interfaces;

public interface ICurrentUser
{
    Guid UserId { get; }
    string Email { get; }
    UserRole Role { get; }
}