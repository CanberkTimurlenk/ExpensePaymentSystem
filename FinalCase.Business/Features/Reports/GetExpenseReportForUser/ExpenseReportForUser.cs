namespace FinalCase.Business.Features.Reports.GetExpenseReportForUser;
public class ExpenseReportForEmployee
{

    public string EmployeeDescription { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string AdminDescription { get; set; }
    public string Status { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public string PaymentMethodName { get; set; }
    public string PaymentMethodDescription { get; set; }
    public string DocumentName { get; set; }
    public string DocumentDescription { get; set; }
    public string DocumentUrl { get; set; }
}