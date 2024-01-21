using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Update;
public class UpdateExpenseCategoryCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateExpenseCategoryCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = await dbContext.FindAsync<ExpenseCategory>(request.Id, cancellationToken);

        if (expenseCategory == null)
            return new ApiResponse(ExpenseCategoryMessages.ExpenseCategoryNotFound);

        expenseCategory.Name = request.Request.Name;
        expenseCategory.Description = request.Request.Description;     

        expenseCategory.UpdateDate = DateTime.Now;
        expenseCategory.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
