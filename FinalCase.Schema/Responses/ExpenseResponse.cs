using FinalCase.Schema.Response;

namespace FinalCase.Schema.Responses;

public record ExpenseResponse
{
    public int Id { get; init; }
    public string EmployeeDescription { get; init; }
    public string AdminDescription { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public string Location { get; init; }
    public int CategoryId { get; init; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public string ExpenseStatus { get; init; } // enum will be converted to string
    public int CreatorEmployeeId { get; init; } // just includes the creator employee id
    // for more information about the creator employee, we need to send another request with the id    
    public ICollection<DocumentResponse> Documents { get; init; }
}
