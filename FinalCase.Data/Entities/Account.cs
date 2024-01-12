using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;

public class Account : BaseEntityWithId
{
    public string IBAN { get; set; }
    public string Balance { get; set; }

    public int FieldEmployeeId { get; set; }
    public ApplicationUser FieldEmployee { get; set; }
}
