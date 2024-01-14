using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using Microsoft.IdentityModel.Tokens;

namespace FinalCase.Business.Features.Expenses.Profiles;
public class ExpenseMappingProfile : Profile
{
    public ExpenseMappingProfile()
    {
        CreateMap<ExpenseRequest, Expense>();

        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Category.Description))
            .ForMember(dest => dest.ExpenseStatus, opt => opt.MapFrom(src => src.Status.ToString()));


        CreateMap<Expense, Payment>()
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
           .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
           .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.CreatorEmployeeId))
           .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ReceiverIban, opt => opt.MapFrom(src => src.CreatorEmployee.Account.Iban))
           .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => $"{src.CreatorEmployee.Firstname} {src.CreatorEmployee.Lastname}"))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Base64UrlEncoder.Encode($"{src.CreatorEmployeeId},{src.Id}")));

    }
}
