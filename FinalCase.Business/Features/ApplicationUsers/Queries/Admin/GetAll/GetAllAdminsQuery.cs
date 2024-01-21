using FinalCase.Base.Response;
using FinalCase.Schema.AppRoles.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
public record GetAllAdminsQuery() : IRequest<ApiResponse<IEnumerable<AdminResponse>>>;