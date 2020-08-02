﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGatewayApi.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public Merchant Merchant { get; set; }
        public Card Card { get; set; }
        public BankResponse BankResponse { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
    }
}
