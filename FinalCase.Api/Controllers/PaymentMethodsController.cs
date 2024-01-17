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

    [HttpGet("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentMethodResponse>> GetPaymentMethodById(int id)
    {
        return await mediator.Send(new GetPaymentMethodByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentMethodResponse>> CreatePaymentMethod(PaymentMethodRequest command)
    {
        return await mediator.Send(new CreatePaymentMethodCommand(command));
    }

    [HttpPut("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdatePaymentMethod(int id, PaymentMethodRequest command)
    {
        return await mediator.Send(new UpdatePaymentMethodCommand(id, command));
    }

    [HttpDelete("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeletePaymentMethod(int id)
    {
        return await mediator.Send(new DeletePaymentMethodCommand(id));
    }
}
