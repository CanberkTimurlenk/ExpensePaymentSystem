using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Create;

public class CreateExpenseCategoryCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var expenseCategory = mapper.Map<ExpenseCategory>(request.Model);

        await dbContext.ExpenseCategories.AddAsync(expenseCategory);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ExpenseCategoryResponse>(expenseCategory);
        return new ApiResponse<ExpenseCategoryResponse>(response);
    }
}