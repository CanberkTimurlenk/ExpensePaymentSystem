using FinalCase.Api.Helpers;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.ExpenseCategories.Commands.Create;
using FinalCase.Business.Features.ExpenseCategories.Commands.Delete;
using FinalCase.Business.Features.ExpenseCategories.Commands.Update;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetAll;
using FinalCase.Business.Features.ExpenseCategories.Queries.GetById;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseCategoriesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Employee}")]
    public async Task<ApiResponse<IEnumerable<ExpenseCategoryResponse>>> GetExpenseCategories()
    {
        return await mediator.Send(new GetAllExpenseCategoriesQuery());
    }

    [HttpGet("{id:min(1)}")]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Employee}")]
    public async Task<ApiResponse<ExpenseCategoryResponse>> GetExpenseCategoryById(int id)
    {
        return await mediator.Send(new GetExpenseCategoryByIdQuery(id));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<ExpenseCategoryResponse>> CreateExpenseCategory(ExpenseCategoryRequest request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int userId))
            return new ApiResponse<ExpenseCategoryResponse>(false);

        return await mediator.Send(new CreateExpenseCategoryCommand(userId, request));
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdateExpenseCategory(int id, ExpenseCategoryRequest request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int userId))
            return new ApiResponse(false);

        return await mediator.Send(new UpdateExpenseCategoryCommand(userId,id, request));
    }

    [HttpDelete("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeleteExpenseCategory(int id)
    {
        return await mediator.Send(new DeleteExpenseCategoryCommand(id));
    }
}
