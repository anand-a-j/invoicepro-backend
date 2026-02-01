namespace InvoicePro.Application.DTOs.Invoice;

public class CreateInvoiceItemRequestDto
{
    public string Description { get; set; } = null!;
    public decimal Amount { get; set; }
}