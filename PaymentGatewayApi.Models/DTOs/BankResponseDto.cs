using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.Models.DTOs
{
    public class BankResponseDto
    {
        public string Identifier { get; set; }
        public string PaymentStatus { get; set; }
        public string Message { get; set; }
    }
}
