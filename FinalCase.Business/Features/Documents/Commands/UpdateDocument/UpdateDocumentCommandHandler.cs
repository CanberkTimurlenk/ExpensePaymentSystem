using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<UpdateDocumentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await dbContext.FindAsync<Document>(request.Id, cancellationToken);

        if (document == null)
            return new ApiResponse(DocumentMessages.DocumentNotFound);

        var expense = await dbContext.Expenses.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.Model.ExpenseId, cancellationToken: cancellationToken);

        if (request.Role == Roles.Employee && expense?.CreatorEmployeeId != request.UpdaterId)
            return new ApiResponse(DocumentMessages.UnauthorizedDocumentUpdate);

        if (expense == null)
            return new ApiResponse(string.Format(DocumentMessages.ExpenseNotFound, request.Model.ExpenseId));

        request.Model.Id = document.Id;

        document.Name = request.Model.Name;
        document.Description = request.Model.Description;
        document.Url = request.Model.Url;
        document.ExpenseId = request.Model.ExpenseId;
        document.UpdateDate = DateTime.Now;
        document.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
