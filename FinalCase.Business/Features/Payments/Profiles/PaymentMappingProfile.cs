using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Schema.ExternalApi;
using Microsoft.IdentityModel.Tokens;

namespace FinalCase.Business.Features.Payments.Profiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, OutgoingPaymentRequest>();

        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.EmployeeExpenseDescription, opt => opt.MapFrom(src => src.Expense.EmployeeDescription))
            .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name));

        CreateMap<PaymentRequest, Payment>();
    }
}
