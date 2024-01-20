using FinalCase.Api.Helpers;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Commands.CreateDocument;
using FinalCase.Business.Features.Documents.Commands.DeleteDocument;
using FinalCase.Business.Features.Documents.Commands.UpdateDocument;
using FinalCase.Business.Features.Documents.Queries.GetAll;
using FinalCase.Business.Features.Documents.Queries.GetById;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<DocumentResponse>>> GetAllDocuments()
    {
        return await mediator.Send(new GetAllDocumentsQuery());
    }

    [HttpGet("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<DocumentResponse>> GetDocumentById(int id)
    {
        return await mediator.Send(new GetDocumentByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<DocumentResponse>> CreateDocument(DocumentRequest request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int userId))
            return new ApiResponse<DocumentResponse>(false);

        return await mediator.Send(new CreateDocumentCommand(userId, request));
    }

    [HttpPut("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdateDocument(int id, DocumentRequest request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int userId))
            return new ApiResponse(false);

        return await mediator.Send(new UpdateDocumentCommand(userId, id, request));
    }

    [HttpDelete("{id:min(1)}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeleteDocument(int id)
    {
        return await mediator.Send(new DeleteDocumentCommand(id));
    }

}
