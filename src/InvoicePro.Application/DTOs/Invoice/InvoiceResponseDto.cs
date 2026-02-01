using InvoicePro.Application.DTOs.Customer;

namespace InvoicePro.Application.DTOs.Invoice;

public class InvoiceResponseDto
{
    public Guid Id {get; set;}
    public string InvoiceNumber {get; set;} = null!;
    public CustomerResponseDto Customer { get; set; } = null!;

    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }

    public decimal SubTotal { get; set; }
    public decimal TotalAmount { get; set; }

    public InvoiceStatus Status { get; set; }

    public List<InvoiceItemResponseDto> Items { get; set; } = new();
}