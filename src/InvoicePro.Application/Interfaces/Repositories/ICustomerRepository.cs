namespace InvoicePro.Interfaces.Data.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(Guid orgId, int page, int pageSize, string? search);
}