using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using System.Threading.Tasks;

namespace PaymentGatewayApi.Service
{
    public interface IPaymentService
    {
        Task<PaymentDetailsDto> GetPaymentDetails(string paymentIdentifier, Merchant currentUser);
        Task<BankResponseDto> PostPayment(PaymentRequestDto paymentRequestDto, Merchant currentUser, string bankUrl);
    }
}