using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;

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
                //TODO dodać ukrywanie danych
                cfg.CreateMap<Card, CardDto>()
                    .ForMember(dest => dest.CardNumber, opts => opts.MapFrom(src => src.CardNumber))
                    .ForMember(dest => dest.CVV, opts => opts.MapFrom(src => src.CVV))
                    .ForMember(dest => dest.ExpiryMonth, opts => opts.MapFrom(src => src.ExpiryMonth))
                    .ForMember(dest => dest.ExpiryYear, opts => opts.MapFrom(src => src.ExpiryYear))
                    .ReverseMap();

                cfg.CreateMap<BankResponse, BankResponseDto>()
                    .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                    .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message))
                    .ForMember(dest => dest.PaymentStatus, opts => opts.MapFrom(src => src.PaymentStatus))
                    .ReverseMap();

                cfg.CreateMap<PaymentDetails, PaymentDetailsDto>()
                    .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                    .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Currency))
                    .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Card, opts => opts.MapFrom(src => src.Card))
                    .ForMember(dest => dest.BankResponse, opts => opts.MapFrom(src => src.BankResponse))
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

    }

}