using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema.Entity.Responses;

/// <summary>
/// The entity "Payment" was created for the reporting purposes,
/// This class was created as a requirement of the case,
/// Although a controller class is not planned, it is added to complete the requirements.
/// </summary>
public class PaymentRequest : BaseResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
    public int ExpenseId { get; set; }
    public int PaymentMethodId { get; set; }
}
