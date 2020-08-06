using Moq;
using PaymentGatewayApi.Controllers;
using PaymentGatewayApi.DataLayer;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using PaymentGatewayApi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGatewayApi.UnitTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentRepository> mockRepo;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly IPaymentService paymentService;

        public PaymentServiceTests()
        {
            this.mockRepo = new Mock<IPaymentRepository>();
            this.mockUnitOfWork = new Mock<IUnitOfWork>();
            this.paymentService = new PaymentService(mockRepo.Object, mockUnitOfWork.Object);
        }

        [Fact]
        public void GetPaymentDetails_Executes_ReturnsPaymentDetails()
        {
            Merchant merchant = new Merchant()
            {
                UserName = "TestUser",
                Email = "test@gmail.com"
            };

            string paymentIdentifier = "12345678";

            mockRepo.Setup(repo => repo.GetPaymentDetails(paymentIdentifier, merchant)).Returns(Task.FromResult(
                new PaymentDetails() {
                Identifier = paymentIdentifier,
                Merchant = merchant,
                Amount = 1000,
                Card = new Card() 
                { 
                    CardNumber = "1234567890123456",
                    Cvv = "111",
                    ExpiryMonth = 1,
                    ExpiryYear = 2020
                },
                Currency = "euro",
                Date = DateTime.Now,
                Status = "SUCCESS"
            }));


            var result = paymentService.GetPaymentDetails(paymentIdentifier, merchant);

            Assert.IsType<Task<PaymentDetailsDto>>(result);
        }

        [Fact]
        public void PostPaymentDetails_Executes_ReturnsPaymentDetails()
        {
            Merchant merchant = new Merchant()
            {
                UserName = "TestUser",
                Email = "test@gmail.com"
            };

            PaymentRequestDto paymentRequestDto = new PaymentRequestDto()
            {
                Amount = 1000,
                Card = new CardDto()
                {
                    CardNumber = "1234567890123456",
                    Cvv = "111",
                    ExpiryMonth = 1,
                    ExpiryYear = 2020
                },
                Currency = "euro"
            };

            string bankUrl = "http://localhost:8080";

            var result = paymentService.PostPayment(paymentRequestDto, merchant, bankUrl);

            Assert.IsType<Task<BankResponseDto>>(result);
        }
    }
}
