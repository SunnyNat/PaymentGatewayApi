using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using AutoMapper;

namespace PaymentGatewayApi.Service
{
    public class PaymentMapper
    {
        private MapperConfiguration _config;
        private Mapper _mapper;

        public PaymentMapper()
        {
            _config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Source, Destination>()
                cfg.CreateMap<Card, CardDto>()
                    .ForMember(dest => dest.CardNumber, opts => opts.MapFrom(src => HideCardNumber(src.CardNumber)))
                    .ForMember(dest => dest.Cvv, opts => opts.MapFrom(src => HideCvv()))
                    .ForMember(dest => dest.ExpiryMonth, opts => opts.MapFrom(src => src.ExpiryMonth))
                    .ForMember(dest => dest.ExpiryYear, opts => opts.MapFrom(src => src.ExpiryYear))
                    .ReverseMap();

                cfg.CreateMap<PaymentDetails, PaymentDetailsDto>()
                    .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                    .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
                    .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency))
                    .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Card, opts => opts.MapFrom(src => src.Card))
                    .ReverseMap();
            });

            _mapper = new Mapper(_config);
        }
        

        internal PaymentDetailsDto MapToPaymentDetailsDto(PaymentDetails paymentDetails)
        {
            return _mapper.Map<PaymentDetailsDto>(paymentDetails);
        }

        internal PaymentDetails MapToPaymentDetails(PaymentDetailsDto paymentDetailsDto)
        {
            return _mapper.Map<PaymentDetails>(paymentDetailsDto);
        }

        private string HideCvv()
        {
            return "***";
        }

        private string HideCardNumber(string cardNumber)
        {
            string last4Digits = cardNumber.Substring(cardNumber.Length - 4, 4);

            return $"****************{last4Digits}";
        }

    }

}