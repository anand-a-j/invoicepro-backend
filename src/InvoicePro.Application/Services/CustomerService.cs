using System.Net;
using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Customer;
using InvoicePro.Application.Exceptions;
using InvoicePro.Application.interfaces;
using InvoicePro.Interfaces.Data.Repositories;

namespace InvoicePro.Application.Services;

class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;

    public CustomerService(ICustomerRepository customerRepository, ICurrentUser currentUser, IUserRepository userRepository)
    {
        _customerRepository = customerRepository;
        _currentUser = currentUser;
        _userRepository = userRepository;
    }

    public async Task<CustomerResponseDto> CreateAsync(CreateCustomerRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId) ??
         throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
               "User does not belong to any organization",
              HttpStatusCode.BadRequest
          );


        var customer = new Customer(
            req.Name,
            user.OrganizationId,
            req.Email,
            req.Phone,
            req.Address
        );

        await _customerRepository.AddAsync(customer);

        var customerResponseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address
        };

        return customerResponseDto;
    }

    public async Task<CustomerResponseDto> UpdateAsync(Guid customerId, UpdateCustomerRequestDto dto)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
       ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );


        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer == null || customer.OrganizationId != user.OrganizationId)
            throw new AppException("Customer not found", HttpStatusCode.NotFound);

        customer.Update(
           dto.Name,
           dto.Email,
           dto.Phone,
           dto.Address
       );

        await _customerRepository.UpdateAsync(customer);

        var customerResponseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address
        };

        return customerResponseDto;
    }

    public async Task DeleteAsync(Guid customerId)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
            ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var customer = await _customerRepository.GetByIdAsync(customerId);

        if (customer == null || customer.OrganizationId != user.OrganizationId)
            throw new AppException("Customer not found", HttpStatusCode.NotFound);

        await _customerRepository.DeleteAsync(customer);
    }

    public async Task<PagedResultDto<CustomerResponseDto>>
    GetListAsync(GetCustomerListRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
        ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var (items, totalCount) = await _customerRepository.GetPagedAsync(
         user.OrganizationId,
         req.Page,
         req.PageSize,
         req.Search
        );

        return new PagedResultDto<CustomerResponseDto>
        {
            Items = items.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
            }
            ).ToList(),
            TotalCount = totalCount,
            Page = req.Page,
            PageSize = req.PageSize
        };
    }
}