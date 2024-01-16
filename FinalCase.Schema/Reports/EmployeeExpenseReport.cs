namespace FinalCase.Schema.Reports;
public record EmployeeExpenseReport
{
    public decimal Amount { get; set; }
    public string EmployeeDescription { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string AdminDescription { get; set; }
    public string ExpenseStatus { get; set; }
    public string CategoryName { get; set; }
    public string PaymentMethodName { get; set; }
    public string DocumentName { get; set; }
    public string DocumentDescription { get; set; }
    public string DocumentUrl { get; set; }

}
