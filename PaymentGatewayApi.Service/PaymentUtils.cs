using System;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using PaymentGatewayApi.DataLayer;
using PaymentGatewayApi.Models.DTOs;
using RestSharp;
using PaymentGatewayApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PaymentGatewayApi.Service
{
    public class PaymentUtils
    {
        private MyDbContext _context;
        private PaymentMapper _paymentMapper;

        public PaymentUtils(MyDbContext context)
        {
            _context = context;
            _paymentMapper = new PaymentMapper();
        }

        public PaymentDetailsDto GetPaymentDetails(string paymentIdentifier)
        {
            PaymentDetails payment = _context.PaymentDetails.Where(p => p.Identifier == paymentIdentifier).FirstOrDefault();

            PaymentDetailsDto paymentDto = _paymentMapper.MapToPaymentDetailsDto(payment);

            return paymentDto;
        }

        public BankResponseDto PostPayment(PaymentDetailsDto paymentDetailsDto, Merchant currentUser, string bankUrl)
        {
            BankResponseDto bankResponseDto = new BankResponseDto();

            var response = CommunicationUtils.ConnectToBank($"{bankUrl}/Payment/Post", Method.POST, paymentDetailsDto);

            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                bankResponseDto = JsonConvert.DeserializeObject<BankResponseDto>(response.Content);
                paymentDetailsDto.BankResponse = bankResponseDto;

                PaymentDetails paymentDetails = _paymentMapper.MapToPaymentDetails(paymentDetailsDto);
                paymentDetails.Merchant = currentUser;

                _context.PaymentDetails.Add(paymentDetails);
            }

            return bankResponseDto;
        }
    }
}
