using FinalCase.Base.Response;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
public record CreateAdminCommand(AdminRequest Model) : IRequest<ApiResponse<AdminResponse>>;