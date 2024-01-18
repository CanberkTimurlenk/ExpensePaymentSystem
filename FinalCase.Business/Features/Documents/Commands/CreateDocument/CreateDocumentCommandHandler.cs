using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Documents.Commands.CreateDocument;
public class CreateDocumentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateDocumentCommand, ApiResponse<DocumentResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    public async Task<ApiResponse<DocumentResponse>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = mapper.Map<Document>(request.Model);

        await dbContext.Documents.AddAsync(document, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<DocumentResponse>(document);
        return new ApiResponse<DocumentResponse>(response);
    }
}
