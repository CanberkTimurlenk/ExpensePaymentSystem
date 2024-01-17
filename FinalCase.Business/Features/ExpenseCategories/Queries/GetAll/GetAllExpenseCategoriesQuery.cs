using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Queries.GetAll;
public record GetAllExpenseCategoriesQuery : IRequest<ApiResponse<IEnumerable<ExpenseCategoryResponse>>>;