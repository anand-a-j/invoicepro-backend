namespace InvoicePro.Application.DTOs.Invoice;

public class InvoiceItemResponseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
}