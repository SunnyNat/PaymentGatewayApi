using System;
using Newtonsoft.Json;
using System.Net;
using PaymentGatewayApi.DataLayer;
using PaymentGatewayApi.Models.DTOs;
using RestSharp;
using PaymentGatewayApi.Models;
using System.Threading.Tasks;

namespace PaymentGatewayApi.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private PaymentMapper paymentMapper;

        public PaymentService(IPaymentRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            paymentMapper = new PaymentMapper();
        }

        public async Task<PaymentDetailsDto> GetPaymentDetails(string paymentIdentifier, Merchant currentUser)
        {
            PaymentDetails payment = await repository.GetPaymentDetails(paymentIdentifier, currentUser);

            if (payment == null) return null;

            PaymentDetailsDto paymentDto = paymentMapper.MapToPaymentDetailsDto(payment);

            return paymentDto;
        }

        public async Task<BankResponseDto> PostPayment(PaymentRequestDto paymentRequestDto, Merchant currentUser, string bankUrl)
        {
            BankResponseDto bankResponseDto = new BankResponseDto();

            var response = CommunicationUtils.ConnectToBank($"{bankUrl}/payment", Method.POST, paymentRequestDto);

            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
            {
                bankResponseDto = JsonConvert.DeserializeObject<BankResponseDto>(response.Content);

                await SavePaymentDetails(paymentRequestDto, bankResponseDto, currentUser);
            }

            return bankResponseDto;
        }

        private async Task SavePaymentDetails(PaymentRequestDto paymentRequestDto, BankResponseDto bankResponseDto, Merchant currentUser)
        {
            PaymentDetails paymentDetails = new PaymentDetails()
            {
                Identifier = bankResponseDto.Identifier,
                Status = bankResponseDto.Status,
                Amount = paymentRequestDto.Amount,
                Card = new Card()
                {
                    CardNumber = paymentRequestDto.Card.CardNumber,
                    Cvv = paymentRequestDto.Card.Cvv,
                    ExpiryMonth = paymentRequestDto.Card.ExpiryMonth,
                    ExpiryYear = paymentRequestDto.Card.ExpiryYear,
                },
                Currency = paymentRequestDto.Currency,
                Merchant = currentUser,
                Date = DateTime.Now
            };

            await repository.PostPaymentDetails(paymentDetails);
            await unitOfWork.CompleteAsync();
        }
    }
}
