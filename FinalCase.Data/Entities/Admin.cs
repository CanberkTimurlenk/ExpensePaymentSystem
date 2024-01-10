using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;

public class Admin : ApplicationUser
{


    // Sistemdeki tüm harcamaları görür. Onaylar veya reddeder.    
    public virtual ICollection<Payment> Payments { get; set; }
    public virtual ICollection<ApprovedExpense> ApprovedExpenses { get; set; }
    public virtual ICollection<RejectedExpense> RejectedExpenses { get; set; }

}
