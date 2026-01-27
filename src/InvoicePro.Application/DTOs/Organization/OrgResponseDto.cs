namespace InvoicePro.Application.DTOs.Organization;

public class OrgResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
}