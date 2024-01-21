using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateDocumentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await dbContext.FindAsync<Document>(request.Id, cancellationToken);

        if (document?.IsActive != true)
            return new ApiResponse(DocumentMessages.DocumentNotFound);

        var expense = await dbContext.Expenses.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.Model.ExpenseId, cancellationToken: cancellationToken);

        if (request.Role == Roles.Employee && expense?.CreatorEmployeeId != request.UpdaterId)
            return new ApiResponse(DocumentMessages.UnauthorizedDocumentUpdate);

        if (expense == null)
            return new ApiResponse(string.Format(DocumentMessages.ExpenseNotFound, request.ExpenseId));

        request.Model.Id = document.Id;
        mapper.Map(request.Model, document);
        document.UpdateDate = DateTime.Now;
        document.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }
}
