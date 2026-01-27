namespace InvoicePro.Interfaces.Data.Repositories;

public interface IOrganizationRepository
{
    Task AddAsync(Organization org);
    Task<Organization?> GetByIdAsync(Guid id);
    Task UpdateAsync(Organization org);
}

