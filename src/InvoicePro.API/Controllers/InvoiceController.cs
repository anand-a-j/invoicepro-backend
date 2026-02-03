using InvoicePro.API.Core;
using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Invoice;
using InvoicePro.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicePro.API.Controllers;

[ApiController]
[Route("api/invoices")]
[Authorize]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceRequestDto req)
    {
        var result = await _invoiceService.CreateAsync(req);
        return Ok(ApiResponse<InvoiceResponseDto>.Ok(
            result,
            "Invoice created successfully"
        ));
    }

    [HttpPut] 
    public async Task<IActionResult> Update([FromBody] UpdateInvoiceRequestDto req)
    {
        var result = await _invoiceService.UpdateAsync(req);
        return Ok(ApiResponse<InvoiceResponseDto>.Ok(
            result,
            "Invoice updated successfully"
        ));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetInvoiceListRequestDto req)
    {
        var result = await _invoiceService.GetListAsync(req);
        return Ok(ApiResponse<PagedResultDto<InvoiceResponseDto>>.Ok(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _invoiceService.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(true, "Invoice deleted successfully"));
    }

    [HttpPost("{id:guid}/mark-paid")]
    public async Task<IActionResult> MarkAsPaid(Guid id)
    {
        await _invoiceService.MarkAsPaidAsync(id);
        return Ok(ApiResponse<bool>.Ok(true, "Invoice marked as paid"));
    }
}