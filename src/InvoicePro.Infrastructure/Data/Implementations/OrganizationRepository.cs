using InvoicePro.Infrastructure;
using InvoicePro.Interfaces.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _db;

    public OrganizationRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Organization org)
    {
        _db.Organizations.Add(org);
        await _db.SaveChangesAsync();
    }

    public async Task<Organization?> GetByIdAsync(Guid id)
    {
        return await _db.Organizations.FirstOrDefaultAsync(x=> x.Id == id);
    }

    public async Task UpdateAsync(Organization org)
    {
        _db.Organizations.Update(org);
        await _db.SaveChangesAsync();
    }
}