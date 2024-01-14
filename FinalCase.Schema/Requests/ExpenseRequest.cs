using FinalCase.Schema.Response;
using System.Text.Json.Serialization;

namespace FinalCase.Schema.Requests;

public class ExpenseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string EmployeeDescription { get; set; } 
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int CategoryId { get; set; } // one to one relationship established

    [JsonIgnore] // will be filled from jwt, jwt den gelecek bu değer !!!
    public int CreatorEmployeeId { get; set; } // her employee yalnızca kendisi için expense oluşturabilir, o yüzden değeri jwt den alacağız

    public ICollection<DocumentRequest> Documents { get; set; }
}
