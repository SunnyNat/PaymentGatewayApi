using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        [StringLength(16)]
        public string CardNumber { get; set; }

        [MinLength(1), MaxLength(2), Range(0, 12)]
        public int ExpiryMonth { get; set; }

        [StringLength(4)]
        public int ExpiryYear { get; set; }

        [StringLength(3)]
        public string Cvv { get; set; }
    }
}
