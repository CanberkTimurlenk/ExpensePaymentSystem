using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;

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





    }
}
