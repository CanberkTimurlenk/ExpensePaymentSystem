using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Constants;
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
        if (!dbContext.Expenses.Any(x => x.Id == request.Model.ExpenseId))
            return new ApiResponse<DocumentResponse>(string.Format(DocumentMessages.ExpenseNotFound, request.Model.ExpenseId));

        var document = mapper.Map<Document>(request.Model);

        document.InsertDate = DateTime.Now;
        document.InsertUserId = request.InsertUserId;

        await dbContext.Documents.AddAsync(document, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<DocumentResponse>(document);
        return new ApiResponse<DocumentResponse>(response);
    }
}
