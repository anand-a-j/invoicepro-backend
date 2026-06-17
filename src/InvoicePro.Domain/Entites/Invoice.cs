using InvoicePro.Domain.Exceptions;

namespace InvoicePro.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid OrganizationId { get; private set; }
    public Guid CustomerId { get; private set; }

    public string InvoiceNumber { get; private set; } = null!;
    public int SequenceNumber { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime? DueDate { get; private set; }

    public decimal SubTotal { get; private set; }
    public decimal TotalAmount { get; private set; }

    public InvoiceStatus Status { get; private set; }
    public List<InvoiceItem> Items { get; set; } = new();

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
        Items.Add(new InvoiceItem(Id, description, amount));
        RecalculateTotals();
    }

    public void ReplaceItems(IEnumerable<InvoiceItem> items)
    {
        EnsureEditable();

        this.Items.Clear();

        foreach (var item in items)
        {
            this.Items.Add(item);
        }

        RecalculateTotals();
    }

    public void UpdateCustomer(Guid customerId)
    {
        EnsureEditable();
        CustomerId = customerId;
    }


    public void UpdateDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
    }

    public void MarkAsPaid()
    {
        if (Status == InvoiceStatus.Paid)
            throw new DomainException("Invoice is already paid.");

        Status = InvoiceStatus.Paid;
    }

    private void RecalculateTotals()
    {
        SubTotal = Items.Sum(x => x.Amount);
        TotalAmount = SubTotal;
    }


    private void EnsureEditable()
    {
        if (Status == InvoiceStatus.Paid)
            throw new DomainException("Paid invoices cannot be modified.");
    }
}