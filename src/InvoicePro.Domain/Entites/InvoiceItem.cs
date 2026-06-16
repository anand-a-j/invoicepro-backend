using InvoicePro.Domain.Exceptions;

namespace InvoicePro.Domain.Entities;

public class InvoiceItem
{
    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }

    private InvoiceItem() { }

    public InvoiceItem(Guid invoiceId, string description, decimal amount)
    {
        if (invoiceId == Guid.Empty)
            throw new DomainException(
            "Invoice not found"
             );

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
           "Invoice item description is required."
            );

        if (amount <= 0)
            throw new DomainException("Invoice item amount must be greater than zero.");

        Id = Guid.NewGuid();
        InvoiceId = invoiceId;
        Description = description;
        Amount = amount;
    }
}