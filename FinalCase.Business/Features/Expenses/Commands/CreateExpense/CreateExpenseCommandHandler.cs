using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using MediatR;
using FinalCase.Data.Entities;

namespace FinalCase.Business.Features.Expenses.Commands.CreateExpense;

public class CreateExpenseCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = mapper.Map<ExpenseRequest, Expense>(request.Model);
        expense.CreatorEmployeeId = request.CreatorEmployeeId;

        await dbContext.Expenses.AddAsync(expense, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<Expense, ExpenseResponse>(expense);
        return new ApiResponse<ExpenseResponse>(response);
    }
}
