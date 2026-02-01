using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Invoice;
using InvoicePro.Application.interfaces;

namespace InvoicePro.Application.Services;

class InvoiceService : IInvoiceService
{    
    public Task<InvoiceResponseDto> CreateAsync(CreateInvoiceRequestDto req)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<InvoiceResponseDto>> GetListAsync(GetInvoiceListRequestDto req)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsPaidAsync(Guid invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> UpdateAsync(UpdateInvoiceRequestDto req)
    {
        throw new NotImplementedException();
    }
}