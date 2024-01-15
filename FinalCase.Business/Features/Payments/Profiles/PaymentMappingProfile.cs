using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Requests;

namespace FinalCase.Business.Features.Payments.Profiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, OutgoingPaymentRequest>();
    }
}
