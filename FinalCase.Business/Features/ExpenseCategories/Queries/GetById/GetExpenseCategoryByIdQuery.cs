using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
public record GetExpenseCategoryByIdQuery(int Id) : IRequest<ApiResponse<ExpenseCategoryResponse>>;