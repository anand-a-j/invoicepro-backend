using InvoicePro.Domain.Entities;

namespace InvoicePro.Interfaces.Data.Repositories;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
    Task<(List<Invoice> Items, int TotalCount)> GetPagedAsync(Guid orgId, int page, int pageSize, string? search);
}