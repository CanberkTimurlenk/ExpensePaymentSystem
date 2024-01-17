using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetAll;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseCategoriesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<ExpenseCategoryResponse>>> GetExpenseCategories()
    {
        return await mediator.Send(new GetAllExpenseCategoriesQuery());
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<ExpenseCategoryResponse>> GetExpenseCategoryById(int id)
    {
        return await mediator.Send(new GetExpenseCategoryByIdQuery(id));
    }

}
