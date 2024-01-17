using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Schema.ExternalApi;

namespace FinalCase.Business.Features.Payments.Profiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, OutgoingPaymentRequest>();

        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.EmployeeExpenseDescription, opt => opt.MapFrom(src => src.Expense.EmployeeDescription))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name))
            .ForMember(dest => dest.PaymentDescription, opt => opt.MapFrom(src => src.Description));


    }
}
