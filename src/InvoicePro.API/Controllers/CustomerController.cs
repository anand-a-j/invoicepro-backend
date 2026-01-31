using InvoicePro.API.Core;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto req)
    {
        var result = await _customerService.CreateAsync(req);
        return Ok(ApiResponse<CustomerResponseDto>.Ok(result, "Customer added successfully"));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Guid customerId, UpdateCustomerRequestDto req)
    {
        var result = await _customerService.UpdateAsync(customerId, req);
        return Ok(ApiResponse<CustomerResponseDto>.Ok(result, "Customer updated successfully"));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] Guid customerId)
    {
        await _customerService.DeleteAsync(customerId);
        return Ok(ApiResponse<bool>.Ok(true,"Customer deleted successfully"));
    }
}