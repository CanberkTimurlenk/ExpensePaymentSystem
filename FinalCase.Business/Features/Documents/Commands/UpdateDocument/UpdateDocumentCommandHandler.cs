using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
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

        if (!dbContext.Expenses.Any(x => x.Id == request.Model.ExpenseId))
            return new ApiResponse(string.Format(DocumentMessages.ExpenseNotFound, request.Model.ExpenseId));

        request.Model.Id = document.Id;
        mapper.Map(request.Model, document);
        document.UpdateDate = DateTime.Now;
        document.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }
}
