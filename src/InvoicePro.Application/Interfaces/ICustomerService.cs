using InvoicePro.Application.DTOs.Customer;

namespace InvoicePro.Application.interfaces;

public interface ICustomerService
{
    Task<CustomerResponseDto> CreateAsync(
        CreateCustomerRequestDto req);
        
    Task<CustomerResponseDto> UpdateAsync(
      Guid customerId,
      UpdateCustomerRequestDto dto,
      Guid orgId);

    Task DeleteAsync(Guid customerId, Guid orgId);
}