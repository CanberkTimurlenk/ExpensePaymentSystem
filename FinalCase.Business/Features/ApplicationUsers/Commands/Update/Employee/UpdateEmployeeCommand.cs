using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.Entity.Requests;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
public record UpdateEmployeeCommand(int UpdaterId, int Id, ApplicationUserRequest Model) : IRequest<ApiResponse>;