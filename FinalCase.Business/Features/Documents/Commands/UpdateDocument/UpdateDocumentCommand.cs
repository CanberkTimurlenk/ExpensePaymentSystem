using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Requests;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.UpdateDocument;
public record UpdateDocumentCommand(int UpdaterId, int Id, DocumentRequest Model) : IRequest<ApiResponse>;