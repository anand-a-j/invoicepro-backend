using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Invoice;

namespace InvoicePro.Application.interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponseDto> CreateAsync(CreateInvoiceRequestDto req);
    Task<InvoiceResponseDto> GetByIdAsync(Guid id);
    Task<InvoiceResponseDto> UpdateAsync(UpdateInvoiceRequestDto req);
    Task DeleteAsync(Guid invoiceId);
    Task MarkAsPaidAsync(Guid invoiceId);
    Task<PagedResultDto<InvoiceResponseDto>> GetListAsync(GetInvoiceListRequestDto req);
}