namespace PaymentGatewayApi.Controllers
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string BankUrl { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}