using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Expenses.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Delete;

public class DeleteExpenseCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await dbContext.FindAsync<Expense>(request.Id, cancellationToken);

        if (request.Role.Equals(Roles.Employee) && expense?.CreatorEmployeeId != request.UserId)
            return new ApiResponse(ExpenseMessages.UnauthorizedExpenseDelete);

        if (expense == null)
            return new ApiResponse(ExpenseMessages.ExpenseNotFound);

        if (expense.Status != ExpenseStatus.Pending)
            return new ApiResponse(ExpenseMessages.OnlyPendingUpdateError);

        expense.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
