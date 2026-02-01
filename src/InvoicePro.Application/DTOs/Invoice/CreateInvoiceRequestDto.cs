namespace InvoicePro.Application.DTOs.Invoice;

public class CreateInvoiceRequestDto
{
    public Guid CustomerId {get; set;}
    public DateTime IssueDate {get;set;}
    public DateTime? DueDate {get; set;}

    public List<CreateInvoiceItemRequestDto> Items {get;set;} = new();
}