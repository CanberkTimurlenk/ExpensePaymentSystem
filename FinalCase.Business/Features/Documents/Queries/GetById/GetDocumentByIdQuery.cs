using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Documents.Queries.GetById;
public record GetDocumentByIdQuery(int Id) : IRequest<ApiResponse<DocumentResponse>>;