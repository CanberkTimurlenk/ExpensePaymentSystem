using FinalCase.Base.Entities;
using FinalCase.Data.Enums;

namespace FinalCase.Data.Entities;
public abstract class Expense : BaseEntity
{

    // Red olan talepler için bir açıklama alanı girişi sağlanmalı
    // ve talep sahibi masraf talebinin neden red olduğunu görebilmeli.    
    public string EmployeeDescription { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public ExpenseStatus Status { get; set; }

    public string Location { get; set; }
    public int CategoryId { get; set; }
    public virtual ExpenseCategory Category { get; set; }

    public int CreatorEmployeeId { get; set; }
    public virtual FieldEmployee CreatorEmployee { get; set; }

    public virtual ICollection<Document> Documents { get; set; }

}
