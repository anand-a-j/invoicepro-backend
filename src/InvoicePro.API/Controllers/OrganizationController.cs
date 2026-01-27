using InvoicePro.API.Core;
using InvoicePro.Application.DTOs.Organization;
using InvoicePro.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicePro.API.Controllers;

[ApiController]
[Route("api/organization")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationSerivce _orgService;

    [HttpPost]
    public async Task<ActionResult<OrgResponseDto>> Create([FromBody] CreateOrgRequestDto req)
    {
        var result = await _orgService.CreateOrganizationAsync(req);
        return Ok(ApiResponse<OrgResponseDto>.Ok(result, "Organization created successfully"));
    }

    [HttpPut]
    public async Task<ActionResult<OrgResponseDto>> Update(UpdateOrgRequestDto req)
    {
        var result = await _orgService.UpdateOrganizationAsync(req);
        return Ok(ApiResponse<OrgResponseDto>.Ok(result, "Organization update successfully"));
    }
}