
namespace PaymentGatewayApi.Models.DTOs
{
    public class PaymentRequestDto
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
        public CardDto Card { get; set; }
    }
}
