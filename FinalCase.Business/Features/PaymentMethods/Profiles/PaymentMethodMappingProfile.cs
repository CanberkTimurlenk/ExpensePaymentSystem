using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Business.Features.PaymentMethods.Profiles;

public class PaymentMethodMappingProfile : Profile
{
    public PaymentMethodMappingProfile()
    {
        CreateMap<PaymentMethod, PaymentMethodResponse>();


        
    }
}
