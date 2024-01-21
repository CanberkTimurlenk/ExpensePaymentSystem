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

        if (expenseCategory == null || !expenseCategory.IsActive)
            return new ApiResponse(ExpenseCategoryMessages.ExpenseCategoryNotFound);

        mapper.Map(request.Request, expenseCategory);
        // since the request is only includes the part of the model that needs to be updated (name,desc)
        // directly map the request to the entity

        expenseCategory.UpdateDate = DateTime.Now;
        expenseCategory.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
