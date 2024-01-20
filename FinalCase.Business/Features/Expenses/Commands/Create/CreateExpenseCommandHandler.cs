using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using MediatR;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Business.Features.Expenses.Constants;

namespace FinalCase.Business.Features.Expenses.Commands.Create;

public class CreateExpenseCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {

        if (!dbContext.ExpenseCategories.Any(x => x.Id == request.Model.CategoryId))
            return new ApiResponse<ExpenseResponse>(ExpenseMessages.CategoryNotFound);

        if (!dbContext.PaymentMethods.Any(x => x.Id == request.Model.PaymentMethodId))
            return new ApiResponse<ExpenseResponse>(ExpenseMessages.PaymentMethodNotFound);

        var expense = mapper.Map<Expense>(request.Model);
        expense.CreatorEmployeeId = request.CreatorEmployeeId;

        expense.InsertDate = DateTime.Now;
        expense.InsertUserId = request.CreatorEmployeeId;

        await dbContext.Expenses.AddAsync(expense, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ExpenseResponse>(expense);
        return new ApiResponse<ExpenseResponse>(response);
    }
}
