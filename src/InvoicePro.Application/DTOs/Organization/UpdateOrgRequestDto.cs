namespace InvoicePro.Application.DTOs.Organization;

public class UpdateOrgRequestDto
{
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}