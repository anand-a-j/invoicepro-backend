using InvoicePro.API.Core;
using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Customer;
using InvoicePro.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicePro.API.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromBody] GetCustomerRequestDto req)
    {
        var result = await _customerService.GetListAsync(req);

        return Ok(ApiResponse<PagedResultDto<CustomerResponseDto>>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto req)
    {
        var result = await _customerService.CreateAsync(req);
        return Ok(ApiResponse<CustomerResponseDto>.Ok(result, "Customer added successfully"));
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> Update(Guid customerId, [FromBody] UpdateCustomerRequestDto req)
    {
        var result = await _customerService.UpdateAsync(customerId, req);
        return Ok(ApiResponse<CustomerResponseDto>.Ok(result, "Customer updated successfully"));
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        await _customerService.DeleteAsync(customerId);
        return Ok(ApiResponse<bool>.Ok(true, "Customer deleted successfully"));
    }
}