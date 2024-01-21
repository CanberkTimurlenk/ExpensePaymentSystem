using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Payments.Commands.Create;
using FinalCase.Business.Features.Payments.Commands.Delete;
using FinalCase.Business.Features.Payments.Commands.Update;
using FinalCase.Business.Features.Payments.Queries.GetAll;
using FinalCase.Business.Features.Payments.Queries.GetById;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentResponse>>> GetPayments()
    {
        return await mediator.Send(new GetAllPaymentsQuery());
    }


    [HttpGet("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentById(int id)
    {
        return await mediator.Send(new GetPaymentByIdQuery(id));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] PaymentRequest model)
    {
        return await mediator.Send(new CreatePaymentCommand(model));
    }


    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdatePayment(int id, [FromBody] PaymentRequest request)
    {
        return await mediator.Send(new UpdatePaymentCommand(id, request));
    }


    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeletePayment(int id)
    {
        return await mediator.Send(new DeletePaymentCommand(id));
    }
}
