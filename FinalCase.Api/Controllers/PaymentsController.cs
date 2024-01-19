﻿using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Commands.Create;
using FinalCase.Business.Features.Payments.Commands.Delete;
using FinalCase.Business.Features.Payments.Commands.Update;
using FinalCase.Business.Features.Payments.Queries.GetAll;
using FinalCase.Business.Features.Payments.Queries.GetById;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentResponse>>> GetPayments()
    {
        return await mediator.Send(new GetAllPaymentsQuery());
    }

    // Composite PK
    [HttpGet("{employee-id:int}/{expense-id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentById([FromRoute(Name = "employee-id")] int employeeId,
        [FromRoute(Name = "expense-id")] int expenseId)
    {
        return await mediator.Send(new GetPaymentByIdQuery(employeeId, expenseId));
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] CreatePaymentCommand command)
    {
        return await mediator.Send(command);
    }

    // Composite PK
    [HttpPut("{employee-id:int}/{expense-id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdatePayment([FromRoute(Name = "employee-id")] int employeeId,
        [FromRoute(Name = "expense-id")] int expenseId, [FromBody] PaymentRequest request)
    {
        return await mediator.Send(new UpdatePaymentCommand(employeeId, expenseId, request));
    }

    // Composite PK
    [HttpDelete("{employee-id:int}/{expense-id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeletePayment([FromRoute(Name = "employee-id")] int employeeId,
        [FromRoute(Name = "expense-id")] int expenseId)
    {
        return await mediator.Send(new DeletePaymentCommand(employeeId, expenseId));
    }
}
