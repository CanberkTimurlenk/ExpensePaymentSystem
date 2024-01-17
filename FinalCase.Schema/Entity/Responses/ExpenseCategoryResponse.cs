using FinalCase.Base.Schema;

namespace FinalCase.Schema.Entity.Responses;

public class ExpenseCategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
}
