using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Commands.CreateDocument;
using FinalCase.Business.Features.Documents.Commands.DeleteDocument;
using FinalCase.Business.Features.Documents.Commands.UpdateDocument;
using FinalCase.Business.Features.Documents.Queries.GetAll;
using FinalCase.Business.Features.Documents.Queries.GetById;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<DocumentResponse>> GetDocumentById(int id)
    {
        return await mediator.Send(new GetDocumentByIdQuery(id));
    }

    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<DocumentResponse>> CreateDocument(DocumentRequest request)
    {
        return await mediator.Send(new CreateDocumentCommand(request));
    }

    [HttpPut("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> UpdateDocument(int id, DocumentRequest request)
    {
        return await mediator.Send(new UpdateDocumentCommand(id, request));
    }

    [HttpDelete("{id:int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> DeleteDocument(int id)
    {
        return await mediator.Send(new DeleteDocumentCommand(id));
    }

}
