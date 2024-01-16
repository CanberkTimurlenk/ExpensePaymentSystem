using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Business.Features.ExpenseCategories.Profiles;
public class ExpenseCategoryMappingProfile : Profile
{
    public ExpenseCategoryMappingProfile()
    {
        CreateMap<ExpenseCategoryRequest, ExpenseCategory>();

        CreateMap<ExpenseCategory, ExpenseCategoryResponse>();
    }
}
