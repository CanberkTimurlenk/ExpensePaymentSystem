using FinalCase.Base.Response;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.DeleteDocument;
public record DeleteDocumentCommand(int Id) : IRequest<ApiResponse>;