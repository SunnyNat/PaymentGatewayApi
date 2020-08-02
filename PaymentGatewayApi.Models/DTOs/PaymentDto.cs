using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.Models.DTOs
{
    public class PaymentDetailsDto
    {
        public string Identifier { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public CardDto Card { get; set; }
        public BankResponseDto BankResponse { get; set; }
        public object Date { get; set; }
    }
}
