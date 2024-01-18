using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Data.Enums;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Commands.Update;

public class UpdateExpenseCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<UpdateExpenseCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await dbContext.FindAsync<Expense>(request.Id);

        if (expense == null)
            return new ApiResponse(ExpenseMessages.ExpenseNotFound);

        if (expense.Status != ExpenseStatus.Pending)
            return new ApiResponse(ExpenseMessages.OnlyPendingUpdateError);

        expense.EmployeeDescription = request.Model.EmployeeDescription;
        expense.Amount = request.Model.Amount;
        expense.Date = request.Model.Date;
        expense.Location = request.Model.Location;
        expense.CategoryId = request.Model.CategoryId;
        expense.PaymentMethodId = request.Model.PaymentMethodId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
