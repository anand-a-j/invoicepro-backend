namespace InvoicePro.Application.DTOs.Customer;

public class GetCustomerRequestDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
}