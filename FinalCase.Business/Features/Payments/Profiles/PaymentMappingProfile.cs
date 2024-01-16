using AutoMapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Requests;

namespace FinalCase.Business.Features.Payments.Profiles;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Payment, OutgoingPaymentRequest>();
    }
}
