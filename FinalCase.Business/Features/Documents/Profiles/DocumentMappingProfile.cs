using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Response;

namespace FinalCase.Business.Features.Documents.Profiles;
public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<DocumentRequest, Document>();


        CreateMap<Document, DocumentResponse>();
    }

}
