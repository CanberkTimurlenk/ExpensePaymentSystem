using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Documents.Commands.CreateDocument;
public class CreateDocumentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateDocumentCommand, ApiResponse<DocumentResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    public async Task<ApiResponse<DocumentResponse>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var expense = await dbContext.Expenses.AsNoTracking()
        .FirstOrDefaultAsync(e => e.Id == request.Model.ExpenseId, cancellationToken: cancellationToken);

        if (request.Role == Roles.Employee && expense?.CreatorEmployeeId != request.InsertUserId)
            return new ApiResponse<DocumentResponse>(DocumentMessages.ExpenseNotBelongingToUser);

        if (expense == null)
            return new ApiResponse<DocumentResponse>(string.Format(DocumentMessages.ExpenseNotFound, request.Model.ExpenseId));


        var document = mapper.Map<Document>(request.Model);

        document.InsertDate = DateTime.Now;
        document.InsertUserId = request.InsertUserId;
        document.IsActive = true;

        await dbContext.Documents.AddAsync(document, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<DocumentResponse>(document);
        return new ApiResponse<DocumentResponse>(response);
    }
}
