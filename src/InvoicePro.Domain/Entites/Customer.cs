using InvoicePro.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }

    public Guid OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public bool IsActive { get; private set; } = true;

    protected Customer() { }

    public Customer(
        string name,
        Guid organizationId,
        string? email = null,
        string? phone = null,
        string? address = null)
    {
        Name = name;
        OrganizationId = organizationId;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public void Update(string name, string? email, string? phone, string? address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}