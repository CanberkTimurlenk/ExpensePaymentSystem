using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Delete;
public class DeleteExpenseCategoryCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = await dbContext.FindAsync<ExpenseCategory>(request.Id, cancellationToken);

        if (dbContext.Expenses.Any(e => e.CategoryId == request.Id))
            return new ApiResponse(ExpenseCategoryMessages.ExpenseCategoryDeleteRestricted);

        if (expenseCategory == null || !expenseCategory.IsActive)
            return new ApiResponse(ExpenseCategoryMessages.ExpenseCategoryNotFound);


        expenseCategory.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
