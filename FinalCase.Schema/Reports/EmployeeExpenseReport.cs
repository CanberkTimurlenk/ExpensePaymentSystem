namespace FinalCase.Schema.Reports;
/// <summary>
/// Schema for
///<para> - GetExpenseReportForEmployeeByDateRange </para>
///<para> - GetEmployeeAllExpensesReport </para>
/// </summary>
public record EmployeeExpenseReport
{
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public string EmployeeDescription { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string AdminDescription { get; set; }
    public string ExpenseStatus { get; set; }
    public string CategoryName { get; set; }
    public string PaymentMethodName { get; set; }
    public List<DocumentReport> Documents { get; set; }

}

public record DocumentReport
{
    public string DocumentName { get; set; }
    public string DocumentDescription { get; set; }
    public string DocumentUrl { get; set; }

}
