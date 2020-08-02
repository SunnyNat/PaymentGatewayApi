using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentGatewayApi.Models
{
    public class BankResponse
    {
        [Key]
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string PaymentStatus { get; set; }
        public string Message { get; set; }
    }
}
