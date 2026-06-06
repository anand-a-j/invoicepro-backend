using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Customer;

namespace InvoicePro.Application.interfaces;

public interface ICustomerService
{
    Task<CustomerResponseDto> CreateAsync(
        CreateCustomerRequestDto req);

    Task<CustomerResponseDto> UpdateAsync(
      Guid customerId,
      UpdateCustomerRequestDto dto);

    Task DeleteAsync(Guid customerId);

    Task<PagedResultDto<CustomerResponseDto>>
    GetListAsync(GetCustomerRequestDto req);
}