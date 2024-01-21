using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Requests;

public class ExpenseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string EmployeeDescription { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int PaymentMethodId { get; set; }
    public int CategoryId { get; set; } 

    [JsonIgnore] // will be filled from jwt
    public int CreatorEmployeeId { get; set; } 

    public ICollection<DocumentRequest> Documents { get; set; }
}
