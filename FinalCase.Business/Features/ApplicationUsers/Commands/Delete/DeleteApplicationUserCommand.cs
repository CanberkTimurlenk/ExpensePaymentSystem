using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Delete;

public record DeleteApplicationUserCommand(int Id) : IRequest<ApiResponse>;