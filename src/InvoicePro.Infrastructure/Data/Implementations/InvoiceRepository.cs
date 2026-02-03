using InvoicePro.Application.DTOs.Customer;
using InvoicePro.Application.DTOs.Invoice;
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

    public async Task<(List<InvoiceResponseDto> Items, int TotalCount)> GetPagedAsync(
        Guid orgId, int page, int pageSize, string? search)
    {
     var query = from invoice in _db.Invoices.AsNoTracking()
            join customer in _db.Customers.AsNoTracking()
            on invoice.CustomerId equals customer.Id
            where invoice.OrganizationId == orgId
            select new { invoice , customer};

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x=>
            x.invoice.InvoiceNumber.Contains(search)
            );
        }

        var totalCount = await query.CountAsync();

        var items = await query.OrderByDescending(x => x.invoice.IssueDate)
                   .Skip((page-1)* pageSize)
                   .Take(pageSize)
                   .Select(x => new InvoiceResponseDto
                   {
                       Id = x.invoice.Id,
                       InvoiceNumber = x.invoice.InvoiceNumber,
                       IssueDate = x.invoice.IssueDate,
                       DueDate = x.invoice.DueDate,
                       SubTotal = x.invoice.SubTotal,
                       TotalAmount = x.invoice.TotalAmount,
                       Status = x.invoice.Status,

                       Customer = new CustomerResponseDto
                       {
                           Id = x.customer.Id,
                           Name = x.customer.Name,
                           Email = x.customer.Email,
                           Phone = x.customer.Phone,
                           Address = x.customer.Address
                       },

                       Items = new List<InvoiceItemResponseDto>()
                   }
                   ).ToListAsync();

                 return (items, totalCount);

    //    var query = _db.Invoices.AsNoTracking().Where(x=> x.OrganizationId == orgId);

    //     if (!string.IsNullOrWhiteSpace(search))
    //     {
    //         query = query.Where(x=> x.InvoiceNumber.Contains(search));
    //     } 

    //     var TotalCount = await query.CountAsync();

    //     var items = await query.OrderByDescending(x => x.IssueDate)
    //     .Skip((page -1) * pageSize)
    //     .Take(pageSize)
    //     .ToListAsync();

    //     return (items, TotalCount);
    }

    public async Task<int?> GetLastSequenceNumberAsync(Guid orgId)
    {
        return await _db.Invoices
        .Where(i => i.OrganizationId == orgId)
        .OrderByDescending(i => i.SequenceNumber)
        .Select(i => i.SequenceNumber)
        .FirstOrDefaultAsync();
    }
}