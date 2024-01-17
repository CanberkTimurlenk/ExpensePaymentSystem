using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Documents.Queries.GetAll;
public record GetAllDocumentsQuery() : IRequest<ApiResponse<IEnumerable<DocumentResponse>>>;