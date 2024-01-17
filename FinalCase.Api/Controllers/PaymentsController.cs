using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Queries.GetAll;
using FinalCase.Business.Features.Payments.Queries.GetById;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers
{
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

        [HttpGet("{employee-id:int}/{expense-id:int}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<PaymentResponse>> GetPaymentById([FromRoute(Name = "employee-id")] int employeeId,
            [FromRoute(Name = "expense-id")] int expenseId)
        {
            return await mediator.Send(new GetPaymentByIdQuery(employeeId, expenseId));
        }

        /*
        [HttpPost]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPut("{" + PaymentId + ":int}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<PaymentResponse>> UpdatePayment([FromRoute(Name = PaymentId)] int paymentId, [FromBody] UpdatePaymentCommand command)
        {
            return await mediator.Send(command with { PaymentId = paymentId });
        }

        [HttpDelete("{" + PaymentId + ":int}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<PaymentResponse>> DeletePayment([FromRoute(Name = PaymentId)] int paymentId)
        {
            return await mediator.Send(new DeletePaymentCommand(paymentId));
        }
        */

    }
}
