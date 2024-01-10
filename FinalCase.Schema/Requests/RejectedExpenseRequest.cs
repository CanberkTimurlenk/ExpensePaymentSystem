namespace FinalCase.Schema.Requests;

public class RejectedExpenseRequest
{
    public string RejectionReason { get; set; }
    public string RejectionDate { get; set; }
    public string EmployeeDescription { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int CategoryId { get; set; }
    public int CreatorEmployeeId { get; set; }
    public ICollection<DocumentRequest> DocumentRequests { get; set; }
}
