using System.Net;
using InvoicePro.Application.DTOs.Common;
using InvoicePro.Application.DTOs.Customer;
using InvoicePro.Application.DTOs.Invoice;
using InvoicePro.Application.Exceptions;
using InvoicePro.Application.interfaces;
using InvoicePro.Application.Mappers;
using InvoicePro.Domain.Entities;
using InvoicePro.Interfaces.Data.Repositories;

namespace InvoicePro.Application.Services;

class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;

    public InvoiceService(
   IInvoiceRepository invoiceRepository,
   ICustomerRepository customerRepository,
   IUserRepository userRepository,
   ICurrentUser currentUser)
    {
        _invoiceRepository = invoiceRepository;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _currentUser = currentUser;
    }


    public async Task<InvoiceResponseDto> CreateAsync(CreateInvoiceRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId) 
        ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var customer = await _customerRepository.GetByIdAsync(req.CustomerId);

        if (customer == null || customer.OrganizationId != user.OrganizationId)
            throw new AppException("Customer not found", HttpStatusCode.NotFound);
        

        var lastSequence = await _invoiceRepository.GetLastSequenceNumberAsync(user.OrganizationId);

        int nextSequence = (int)(lastSequence != null ? lastSequence + 1 : 1);

        var invoiceNumber = $"INV-{nextSequence:D6}";

        var invoice = new Invoice(
            user.OrganizationId,
            req.CustomerId,
            invoiceNumber,
            nextSequence,
            req.IssueDate,
            req.DueDate
        );

        foreach (var item in req.Items)
        {
            invoice.AddItem(item.Description, item.Amount);
        }

        await _invoiceRepository.AddAsync(invoice);

        var customerResponseDto = new CustomerResponseDto{
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address
        };

        return InvoiceMapper.MapToResponse(invoice, customerResponseDto);
    }



    public Task DeleteAsync(Guid invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<InvoiceResponseDto>> GetListAsync(GetInvoiceListRequestDto req)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsPaidAsync(Guid invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceResponseDto> UpdateAsync(UpdateInvoiceRequestDto req)
    {
        throw new NotImplementedException();
    }
}