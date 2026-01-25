using InvoicePro.Domain.Enums;

namespace InvoicePro.Application.DTOs.Auth;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Owner;
}