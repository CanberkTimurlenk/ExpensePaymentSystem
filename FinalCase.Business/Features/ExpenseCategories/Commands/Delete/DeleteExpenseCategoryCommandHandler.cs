using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Delete;
public class DeleteExpenseCategoryCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = await dbContext.FindAsync<ExpenseCategory>(request.Id, cancellationToken);

        if (expenseCategory == null)
            return new ApiResponse(ExpenseCategoryMessages.ExpenseCategoryNotFound);

        expenseCategory.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
