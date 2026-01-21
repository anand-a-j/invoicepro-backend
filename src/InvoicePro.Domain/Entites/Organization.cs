using InvoicePro.Domain.Entities;

public class Organization : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Address { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public bool IsActive { get; private set; } = true;

    protected Organization() { }

    public Organization(string name, string? address = null)
    {
        Name = name;
        Address = address;
    }

    public void UpdateContact(string? email, string? phone)
    {
        Email = email;
        Phone = phone;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}