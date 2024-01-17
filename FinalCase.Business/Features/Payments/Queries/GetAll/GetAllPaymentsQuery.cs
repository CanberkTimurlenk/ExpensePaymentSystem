using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Payments.Queries.GetAll;
public record GetAllPaymentsQuery : IRequest<ApiResponse<IEnumerable<PaymentResponse>>>;