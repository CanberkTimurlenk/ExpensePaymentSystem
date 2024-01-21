using FinalCase.Base.Entities;
using FinalCase.Data.Enums;

namespace FinalCase.Data.Entities;
public class Expense : BaseEntityWithId
{    
    public string? EmployeeDescription { get; set; } // employee could be add description
    public string? AdminDescription { get; set; } // admin could be add description such as the rejection reason
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public ExpenseStatus Status { get; set; }
    public string Location { get; set; }

    public int CategoryId { get; set; }
    public virtual ExpenseCategory Category { get; set; }

    public int CreatorEmployeeId { get; set; }
    public virtual ApplicationUser CreatorEmployee { get; set; }

    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }

    public int? ReviewerAdminId { get; set; }
    public virtual ApplicationUser? ReviewerAdmin { get; set; }

    public Payment? Payment { get; set; }

    public virtual ICollection<Document> Documents { get; set; }
}