using InvoicePro.Application.DTOs.Organization;

namespace InvoicePro.Application.interfaces;

public interface IOrganizationSerivce
{
    Task<OrgResponseDto> CreateOrganizationAsync(CreateOrgRequestDto req);
    Task<OrgResponseDto> UpdateOrganizationAsync(UpdateOrgRequestDto dto);
}