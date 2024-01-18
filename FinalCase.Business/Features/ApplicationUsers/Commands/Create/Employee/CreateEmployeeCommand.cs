using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
public record CreateEmployeeCommand(EmployeeRequest Model) : IRequest<ApiResponse<EmployeeResponse>>;