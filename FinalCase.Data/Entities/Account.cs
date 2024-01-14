using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;

public class Account : BaseEntityWithId
{
    public string Iban { get; set; }
    public int FieldEmployeeId { get; set; }
    public ApplicationUser FieldEmployee { get; set; }
}
