using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using MediatR;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Schema.Entity.Requests;

namespace FinalCase.Business.Features.Expenses.Commands.Create;

public class CreateExpenseCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = mapper.Map<Expense>(request.Model);
        expense.CreatorEmployeeId = request.CreatorEmployeeId;

        await dbContext.Expenses.AddAsync(expense, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ExpenseResponse>(expense);
        return new ApiResponse<ExpenseResponse>(response);
    }
}
