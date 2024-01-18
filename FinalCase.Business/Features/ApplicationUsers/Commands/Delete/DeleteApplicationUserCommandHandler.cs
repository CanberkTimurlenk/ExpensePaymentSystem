using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
public class DeleteApplicationUserCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<DeleteApplicationUserCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var applicationUser = await dbContext.FindAsync<ApplicationUser>(request.Id, cancellationToken);

        if (applicationUser == null)
            return new ApiResponse(ApplicationUserMessages.UserNotFound);

        applicationUser.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
