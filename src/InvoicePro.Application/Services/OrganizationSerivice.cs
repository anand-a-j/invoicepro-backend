using System.Data;
using System.Net;
using System.Net.Cache;
using InvoicePro.Application.DTOs.Organization;
using InvoicePro.Application.Exceptions;
using InvoicePro.Application.interfaces;
using InvoicePro.Domain.Entities;
using InvoicePro.Domain.Enums;
using InvoicePro.Interfaces.Data.Repositories;

namespace InvoicePro.Application.Services;

class OrganizationService : IOrganizationSerivce
{
    private readonly IUserRepository _userRepository;
    private readonly IOrganizationRepository _orgRepository;
    private readonly ICurrentUser _currentUser;

    public OrganizationService(
      IUserRepository userRepository,
      IOrganizationRepository orgRepository,
      ICurrentUser currentUser
  )
    {
        _userRepository = userRepository;
        _orgRepository = orgRepository;
        _currentUser = currentUser;
    }

    public async Task<OrgResponseDto> CreateOrganizationAsync(CreateOrgRequestDto req)
    {

        var user = await _userRepository.GetByIdAsync(_currentUser.UserId) ??
         throw new AppException("User not found", HttpStatusCode.NotFound);


        Console.WriteLine(user.OrganizationId + "org id ");

        if (user.OrganizationId != Guid.Empty && user.OrganizationId != null)
            throw new AppException(
              "User already has an organization",
              HttpStatusCode.BadRequest
          );

        var organization = new Organization(req.Name, req.Address);
        organization.UpdateContact(req.Email, req.Phone);

        await _orgRepository.AddAsync(organization);

        user.AssignOrganization(organization);
        await _userRepository.UpdateAsync(user);

        var orgResponseDto = new OrgResponseDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Email = organization.Email,
            Address = organization.Address,
            Phone = organization.Phone,
            IsActive = organization.IsActive,
        };

        return orgResponseDto;
    }

    public async Task<OrgResponseDto> UpdateOrganizationAsync(UpdateOrgRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
        ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId != Guid.Empty && user.OrganizationId != null)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        if (_currentUser.Role != UserRole.Owner)
            throw new AppException(
                "Only owner can update organization",
                HttpStatusCode.Forbidden
            );

        if (user.OrganizationId == null)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var orgId = user.OrganizationId.Value;

        var organization = await _orgRepository.GetByIdAsync(orgId)
      ?? throw new AppException("Organization not found", HttpStatusCode.NotFound);

        if (req.Name is not null)
            organization.UpdateName(req.Name);


        if (req.Address is not null)
            organization.UpdateAddress(req.Address);

        organization.UpdateContact(
            req.Email ?? organization.Email,
            req.Phone ?? organization.Phone
        );

        await _orgRepository.UpdateAsync(organization);

        var orgResponseDto = new OrgResponseDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Email = organization.Email,
            Address = organization.Address,
            Phone = organization.Phone,
            IsActive = organization.IsActive,
        };

        return orgResponseDto;
    }
}