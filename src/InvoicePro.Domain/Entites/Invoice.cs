using InvoicePro.Domain.Exceptions;

namespace InvoicePro.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid OrganizationId { get; private set; }
    public Guid CustomerId { get; private set; }

    public string InvoiceNumber {get; private set; } = null!;
    public int SequenceNumber { get; private set; }
    public DateTime IssueDate {get; private set;}
    public DateTime? DueDate {get; private set;}

    public decimal SubTotal {get; private set;}
    public decimal TotalAmount { get; private set;}

    public InvoiceStatus Status { get; private set; }
    private readonly List<InvoiceItem> _items = new();
    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();


    private Invoice() { }

    public Invoice(
       Guid organizationId,
       Guid customerId,
       string invoiceNumber,
       int sequenceNumber,
       DateTime issueDate,
       DateTime? dueDate
   )
    {
        Id = Guid.NewGuid();
        OrganizationId = organizationId;
        CustomerId = customerId;
        InvoiceNumber = invoiceNumber;
        SequenceNumber = sequenceNumber;
        IssueDate = issueDate;
        DueDate = dueDate;

        Status = InvoiceStatus.Draft;
    }

    public void AddItem(string description, decimal amount)
    {
        EnsureEditable();

        _items.Add(new InvoiceItem(description, amount));

        RecalculateTotals();
    }


    public void MarkAsPaid()
    {
        if (Status == InvoiceStatus.Paid)
            throw new DomainException("Invoice is already paid.");

        Status = InvoiceStatus.Paid;
    }

    private void RecalculateTotals()
    {
        SubTotal = _items.Sum(x => x.Amount);
        TotalAmount = SubTotal;
    }


    private void EnsureEditable()
    {
        if (Status == InvoiceStatus.Paid)
            throw new DomainException("Paid invoices cannot be modified.");
    }
}