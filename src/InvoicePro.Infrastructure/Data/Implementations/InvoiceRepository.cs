using InvoicePro.Domain.Entities;
using InvoicePro.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoicePro.Infrastructure.Implementations;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _db;

    public InvoiceRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Invoice invoice)
    {
        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();
    }

    public async Task<Invoice?> GetByIdAsync(Guid id)
    {
        return await _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Invoice invoice)
    {
        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync();
    }

    public async Task<(List<Invoice> Items, int TotalCount)> GetPagedAsync(
        Guid orgId, int page, int pageSize, string? search)
    {
       var query = _db.Invoices.AsNoTracking().Where(x=> x.OrganizationId == orgId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x=> x.InvoiceNumber.Contains(search));
        } 

        var TotalCount = await query.CountAsync();

        var items = await query.OrderByDescending(x => x.IssueDate)
        .Skip((page -1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        return (items, TotalCount);
    }
}