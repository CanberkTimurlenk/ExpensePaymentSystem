using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Documents.Queries.GetById;
public record GetDocumentByIdQuery(int UserId, string Role, int Id) : IRequest<ApiResponse<DocumentResponse>>;