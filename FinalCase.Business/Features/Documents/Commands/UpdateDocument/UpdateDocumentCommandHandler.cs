using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateDocumentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await dbContext.FindAsync<Document>(request.Id, cancellationToken);

        if (document is null)
            return new ApiResponse(DocumentMessages.DocumentNotFound);

        request.Model.Id = document.Id;
        mapper.Map(request.Model, document);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }
}
