using FinalCase.Base.Response;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
public record GetApplicationUserByIdQuery(int Id) : IRequest<ApiResponse<ApplicationUserResponse>>;