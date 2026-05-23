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

        if (user.OrganizationId == null)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );


        var orgId = user.OrganizationId.Value;


        if (customer == null || customer.OrganizationId != orgId)
            throw new AppException("Customer not found", HttpStatusCode.NotFound);


        var lastSequence = await _invoiceRepository.GetLastSequenceNumberAsync(orgId);

        int nextSequence = (int)(lastSequence != null ? lastSequence + 1 : 1);

        var invoiceNumber = $"INV-{nextSequence:D6}";

        var invoice = new Invoice(
            orgId,
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

        var customerResponseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address
        };

        return InvoiceMapper.MapToResponse(invoice, customerResponseDto);
    }

    public async Task<InvoiceResponseDto> UpdateAsync(UpdateInvoiceRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
      ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == Guid.Empty)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var invoice = await _invoiceRepository.GetByIdAsync(req.InvoiceId);

        if (invoice == null || invoice.OrganizationId != user.OrganizationId)
            throw new AppException("Invoice not found", HttpStatusCode.NotFound);

        var customer = await _customerRepository.GetByIdAsync(invoice.CustomerId)
         ?? throw new AppException("Customer not found", HttpStatusCode.NotFound);

        if (invoice.Status != InvoiceStatus.Draft)
            throw new AppException(
               "Only draft invoices can be updated",
               HttpStatusCode.BadRequest
            );


        if (req.CustomerId != customer.Id)
        {
            invoice.UpdateCustomer(req.CustomerId);
        }

        if (req.DueDate != invoice.DueDate)
        {
            invoice.UpdateDueDate(req.DueDate);
        }

        var newItems = req.Items.Select(i =>
        new InvoiceItem(i.Description, i.Amount)
        );

        invoice.ReplaceItems(newItems);

        await _invoiceRepository.UpdateAsync(invoice);

        var customerResponseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address
        };

        return InvoiceMapper.MapToResponse(invoice, customerResponseDto);
    }

    public async Task<InvoiceResponseDto> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
           ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        var invoice = await _invoiceRepository.GetByIdAsync(id);

        if (invoice == null || invoice.OrganizationId != user.OrganizationId)
            throw new AppException("Invoice not found", HttpStatusCode.NotFound);

        var customer = await _customerRepository.GetByIdAsync(invoice.CustomerId)
        ?? throw new AppException("Customer not found", HttpStatusCode.NotFound);

        var customerResponseDto = new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address
        };

        return InvoiceMapper.MapToResponse(invoice, customerResponseDto);
    }

    public async Task DeleteAsync(Guid invoiceId)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
         ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

        if (invoice == null || invoice.OrganizationId != user.OrganizationId)
            throw new AppException("Invoice not found", HttpStatusCode.NotFound);

        if (invoice.Status != InvoiceStatus.Draft)
            throw new AppException(
                "Only draft invoices can be deleted",
                HttpStatusCode.BadRequest
            );

        await _invoiceRepository.DeleteAsync(invoice);
    }

    public async Task MarkAsPaidAsync(Guid invoiceId)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId)
             ?? throw new AppException("User not found", HttpStatusCode.NotFound);

        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

        if (invoice == null || invoice.OrganizationId != user.OrganizationId)
            throw new AppException("Invoice not found", HttpStatusCode.NotFound);

        invoice.MarkAsPaid();

        await _invoiceRepository.UpdateAsync(invoice);
    }

    public async Task<PagedResultDto<InvoiceResponseDto>> GetListAsync(GetInvoiceListRequestDto req)
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.UserId) ??
         throw new AppException("User not found", HttpStatusCode.NotFound);

        if (user.OrganizationId == null)
            throw new AppException(
                "User does not belong to any organization",
                HttpStatusCode.BadRequest
            );

        var orgId = user.OrganizationId.Value;

        var (items, totalCount) = await _invoiceRepository.GetPagedAsync(
          orgId,
          req.Page,
          req.PageSize,
          req.Search
        );

        return new PagedResultDto<InvoiceResponseDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = req.Page,
            PageSize = req.PageSize
        };
    }
}