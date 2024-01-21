using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Commands.Create;
using FinalCase.Business.Features.PaymentMethods.Commands.Delete;
using FinalCase.Business.Features.PaymentMethods.Commands.Update;
using FinalCase.Business.Features.PaymentMethods.Queries.GetAll;
using FinalCase.Business.Features.PaymentMethods.Queries.GetById;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static FinalCase.Api.Helpers.ClaimsHelper;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentMethodResponse>>> GetPaymentMethods()
    {
        return await mediator.Send(new GetAllPaymentMethodsQuery());
    }

    [HttpGet("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentMethodResponse>> GetPaymentMethodById(int id)
    {
        return await mediator.Send(new GetPaymentMethodByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentMethodResponse>> CreatePaymentMethod(PaymentMethodRequest command)
    {
        var (userId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);

        return await mediator.Send(new CreatePaymentMethodCommand(userId, command));
    }

    [HttpPut("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdatePaymentMethod(int id, PaymentMethodRequest command)
    {
        var (userId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);

        return await mediator.Send(new UpdatePaymentMethodCommand(userId, id, command));
    }

    [HttpDelete("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeletePaymentMethod(int id)
    {
        return await mediator.Send(new DeletePaymentMethodCommand(id));
    }
}
