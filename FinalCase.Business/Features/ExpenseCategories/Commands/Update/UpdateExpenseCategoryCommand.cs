using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.ExpenseCategories.Commands.Update;
public record UpdateExpenseCategoryCommand(int Id,ExpenseCategoryRequest Request) : IRequest<ApiResponse>;