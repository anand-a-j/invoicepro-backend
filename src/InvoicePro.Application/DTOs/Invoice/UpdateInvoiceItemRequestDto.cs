namespace InvoicePro.Application.DTOs.Invoice;

public class UpdateInvoiceItemRequestDto
{
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
}