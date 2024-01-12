using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;

namespace FinalCase.Business.Features.ExpenseCategories.Profiles;
public class ExpenseCategoryMappingProfile : Profile
{
    public ExpenseCategoryMappingProfile()
    {
        CreateMap<ExpenseCategoryRequest, ExpenseCategory>();

        CreateMap<ExpenseCategory, ExpenseCategoryResponse>();
    }
}
