using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;
public class PaymentMethod : BaseEntityWithId
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Expense> Expenses { get; set; }
    public ICollection<Payment> Payments { get; set; }
}