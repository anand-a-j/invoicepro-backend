using InvoicePro.Application.DTOs.Customer;
using InvoicePro.Application.DTOs.Invoice;
using InvoicePro.Domain.Entities;

namespace InvoicePro.Application.Mappers;

public static class InvoiceMapper
{
    public static InvoiceResponseDto MapToResponse(
     Invoice invoice,
     CustomerResponseDto customer)
    {
        return new InvoiceResponseDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            Customer = customer,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.DueDate,
            SubTotal = invoice.SubTotal,
            TotalAmount = invoice.TotalAmount,
            Status = invoice.Status,
            Items = invoice.Items.Select(i => new InvoiceItemResponseDto
            {
                Id = i.Id,
                Description = i.Description,
                Amount = i.Amount
            }).ToList()
        };
    }
}