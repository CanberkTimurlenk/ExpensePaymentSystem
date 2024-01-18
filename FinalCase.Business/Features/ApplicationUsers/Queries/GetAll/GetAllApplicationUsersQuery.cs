using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
public record GetAllApplicationUsersQuery() : IRequest<ApiResponse<IEnumerable<ApplicationUserResponse>>>;