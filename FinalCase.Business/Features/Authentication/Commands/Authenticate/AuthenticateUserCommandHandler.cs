using FinalCase.Base.Helpers.Encryption;
using FinalCase.Base.Response;
using FinalCase.Base.Token;
using FinalCase.Business.Features.Authentication.Commands;
using FinalCase.Business.Features.Authentication.Constants.Messages;
using FinalCase.Business.Features.Authentication.Helpers;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinalCase.Business.Features.Authentication;
public class AuthenticateUserCommandHandler(FinalCaseDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
    : IRequestHandler<AuthenticateUserCommand, ApiResponse<AuthenticationResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly JwtConfig jwtConfig = jwtConfig.CurrentValue;
    public async Task<ApiResponse<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<ApplicationUser>()
            .FirstOrDefaultAsync(user => user.Username.Equals(request.Model.Username), cancellationToken);

        if (user == null)
            return new ApiResponse<AuthenticationResponse>(AuthenticationMessages.InvalidCredentials);

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());

        if (!hash.Equals(user.Password))
        {
            user.LastActivityDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<AuthenticationResponse>(AuthenticationMessages.InvalidCredentials);
        }
        user.LastActivityDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse<AuthenticationResponse>(new AuthenticationResponse()
        {
            Email = user.Email,
            Token = TokenHelper.CreateAccessToken(user, jwtConfig),
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        });
    }
}