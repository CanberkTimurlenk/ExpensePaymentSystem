using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Create;

public class CreatePaymentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreatePaymentCommand, ApiResponse>
{



}
