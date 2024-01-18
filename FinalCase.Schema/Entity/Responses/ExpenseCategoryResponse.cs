using FinalCase.Base.Schema;

namespace FinalCase.Schema.Entity.Responses;

public class ExpenseCategoryResponse : BaseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
