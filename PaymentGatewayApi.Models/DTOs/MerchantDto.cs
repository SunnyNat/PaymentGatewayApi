using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.Models.DTOs
{
    public class MerchantDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
