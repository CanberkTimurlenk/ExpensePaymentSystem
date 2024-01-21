using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
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
            .ForMember(dest => dest.ExpenseStatus, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.MethodId, opt => opt.MapFrom(src => src.PaymentMethod.Id))
            .ForMember(dest => dest.MethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name))
            .ForMember(dest => dest.MethodDescription, opt => opt.MapFrom(src => src.PaymentMethod.Description));

        CreateMap<Expense, Payment>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
           .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.CreatorEmployeeId))
           .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.ReceiverIban, opt => opt.MapFrom(src => src.CreatorEmployee.Iban))
           .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
           .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name))
           .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => $"{src.CreatorEmployee.Firstname} {src.CreatorEmployee.Lastname}"))
           .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.CreatorEmployee))
           .ForMember(dest => dest.Expense, opt => opt.MapFrom(src => src));
    }
}
