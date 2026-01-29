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
}

