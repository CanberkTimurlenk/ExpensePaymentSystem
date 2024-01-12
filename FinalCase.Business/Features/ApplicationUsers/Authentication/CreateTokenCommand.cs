using FinalCase.Base.Response;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Authentication;

public record AuthenticateUserCommand(AuthenticationRequest Model) : IRequest<ApiResponse<AuthenticationResponse>>;