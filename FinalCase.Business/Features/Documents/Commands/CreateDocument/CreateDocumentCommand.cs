using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.CreateDocument;

public record CreateDocumentCommand(int InsertUserId, DocumentRequest Model) : IRequest<ApiResponse<DocumentResponse>>;