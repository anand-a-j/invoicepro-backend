namespace InvoicePro.Application.DTOs.Invoice;

public class UpdateInvoiceRequestDto
{
    public Guid CustomerId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }

    public List<UpdateInvoiceItemRequestDto> Items { get; set; } = new();
}