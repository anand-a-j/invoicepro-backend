using InvoicePro.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoicePro.Infrastructure.Implementations;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _db;

    public CustomerRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Customer customer)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        customer.Deactivate();
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();
    }

    public async Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(Guid orgId, int page, int pageSize, string? search)
    {
        var query = _db.Customers.Where(x => x.OrganizationId == orgId && x.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()) ||
             (x.Email != null && x.Email.ToLower().Contains(search.ToLower())));
        }

        var totalCount = await query.CountAsync();

        var items = await query.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, totalCount);
    }
}