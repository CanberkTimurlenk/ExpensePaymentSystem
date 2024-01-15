using FinalCase.Business.Features.Expenses.Commands.ApprovePendingExpenses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApprovesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApprovesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("approve")]
    public async Task<IActionResult> ApproveExpenses([FromBody] ApproveExpensesCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
