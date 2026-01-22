using InvoicePro.Domain.Enums;

namespace InvoicePro.Domain.Entities;

public class User : BaseEntity
{
    public string Email {get; private set;} = null!;
    public string PasswordHash {get; private set;} = null!;
    public string FullName {get; private set; } = null!;
    public UserRole Role {get; private set;} = UserRole.Owner;
    public Guid OrganizationId {get; private set;}
    public Organization Organization { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    protected User() { }

    public User(
       string email,
       string passwordHash,
       string fullName,
       Guid organizationId,
       UserRole role = UserRole.Owner)
    {
        Email = email.ToLowerInvariant();
        PasswordHash = passwordHash;
        FullName = fullName;
        OrganizationId = organizationId;
        Role = role;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;
    }
}