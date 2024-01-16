using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Business.Features.Documents.Profiles;
public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<DocumentRequest, Document>();


        CreateMap<Document, DocumentResponse>();
    }

}
