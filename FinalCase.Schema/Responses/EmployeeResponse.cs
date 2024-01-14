namespace FinalCase.Schema.Responses;

public record EmployeeResponse
{
    public string IBAN { get; set; }

    //public virtual ICollection<Expense> Expenses { get; set; }
    //public virtual ICollection<Payment> Payments { get; set; }
}
