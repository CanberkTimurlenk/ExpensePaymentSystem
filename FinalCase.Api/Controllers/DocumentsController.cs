using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Documents.Commands.CreateDocument;
using FinalCase.Business.Features.Documents.Commands.DeleteDocument;
using FinalCase.Business.Features.Documents.Commands.UpdateDocument;
using FinalCase.Business.Features.Documents.Queries.GetAll;
using FinalCase.Business.Features.Documents.Queries.GetById;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using static FinalCase.Api.Helpers.ClaimsHelper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<DocumentResponse>>> GetAllDocuments()
    {
        return await mediator.Send(new GetAllDocumentsQuery());
    }

    [HttpGet("{id:min(1)}")]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public async Task<ApiResponse<DocumentResponse>> GetDocumentById(int id)
    {
        var (userId, role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);

        return await mediator.Send(new GetDocumentByIdQuery(userId, role, id));
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public async Task<ApiResponse<DocumentResponse>> CreateDocument(DocumentRequest request)
    {
        var (userId, role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add InsertUserId

        return await mediator.Send(new CreateDocumentCommand(userId, role, request));
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public async Task<ApiResponse> UpdateDocument(int id, DocumentRequest request)
    {
        var (userId, role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add UpdateUserId
        return await mediator.Send(new UpdateDocumentCommand(userId, role, id, request));
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Employee}")]
    public async Task<ApiResponse> DeleteDocument(int id)
    {
        var (userId, role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        return await mediator.Send(new DeleteDocumentCommand(userId, role, id));
    }

}
