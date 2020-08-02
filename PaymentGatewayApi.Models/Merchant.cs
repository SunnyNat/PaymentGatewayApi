using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PaymentGatewayApi.Models
{
    public class Merchant: IdentityUser
    {
        public List<PaymentDetails> PaymentDetails { get; set; }
    }
}
