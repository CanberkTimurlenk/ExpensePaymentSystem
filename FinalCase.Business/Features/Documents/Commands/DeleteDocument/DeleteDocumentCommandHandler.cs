using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.DeleteDocument;
public class DeleteDocumentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<DeleteDocumentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await dbContext.FindAsync<Document>(request.Id, cancellationToken);

        if (document == null)
            return new ApiResponse(DocumentMessages.DocumentNotFound);

        document.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
