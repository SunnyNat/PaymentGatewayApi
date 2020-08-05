using PaymentGatewayApi.Models;
using System.Threading.Tasks;

namespace PaymentGatewayApi.DataLayer
{
    public interface IPaymentRepository
    {
        Task<PaymentDetails> GetPaymentDetails(string paymentIdentifier, Merchant currentUser);
        Task PostPaymentDetails(PaymentDetails paymentDetails);
    }
}