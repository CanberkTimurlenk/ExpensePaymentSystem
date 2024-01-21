using FinalCase.Base.Schema;

namespace FinalCase.Schema.Entity.Responses;

public class ExpenseResponse : BaseResponse
{
    public int Id { get; set; }
    public string EmployeeDescription { get; set; }
    public string AdminDescription { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public string ExpenseStatus { get; set; } // enum will be converted to string
    public int CreatorEmployeeId { get; set; } 
    public int MethodId { get; set; }
    public string MethodName { get; set; }
    public string MethodDescription { get; set; }
    public ICollection<DocumentResponse> Documents { get; set; }


}
