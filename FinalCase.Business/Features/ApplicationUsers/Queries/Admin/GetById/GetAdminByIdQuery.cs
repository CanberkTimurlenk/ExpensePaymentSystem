using FinalCase.Base.Response;
using FinalCase.Schema.AppRoles.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
public record GetAdminByIdQuery(int Id, bool IncludeDeleted) : IRequest<ApiResponse<AdminResponse>>;