using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Business.Features.ApplicationUsers.Profiles;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, AdminResponse>();

        CreateMap<AdminRequest, ApplicationUser>();

        CreateMap<ApplicationUser, EmployeeResponse>();

        CreateMap<EmployeeRequest, ApplicationUser>();

        CreateMap<ApplicationUser, ApplicationUserResponse>();

    }

}
