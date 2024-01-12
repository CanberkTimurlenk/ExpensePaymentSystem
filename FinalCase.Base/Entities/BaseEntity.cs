
namespace FinalCase.Base.Entities;
public abstract class BaseEntity
{
    public int? InsertUserId { get; set; }
    public DateTime? InsertDate { get; set; }
    public int? UpdateUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsActive { get; set; }
}
