using FinalCase.Base.Response;
using FinalCase.Schema.Authentication;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Authentication;

public record AuthenticateUserCommand(AuthenticationRequest Model) : IRequest<ApiResponse<AuthenticationResponse>>;