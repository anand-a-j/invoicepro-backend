namespace InvoicePro.Interfaces.Data.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}