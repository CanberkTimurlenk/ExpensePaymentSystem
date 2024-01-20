using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
public record UpdateAdminCommand(int UpdaterId,int Id, ApplicationUserRequest Model) : IRequest<ApiResponse>;