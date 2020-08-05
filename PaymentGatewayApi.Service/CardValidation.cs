using CreditCardValidator;
using PaymentGatewayApi.Models.DTOs;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PaymentGatewayApi.Service
{
    public class CardValidation
    {
        public bool IsValid { get; }
        public CardValidation(CardDto cardDto)
        {
            IsValid = IsCardValid(cardDto);
        }

        private bool IsCardValid(CardDto card)
        {
            return NumberValid(card.CardNumber) && DateValid(card) && CvvValid(card.Cvv);
        }

        private bool NumberValid(string cardNumber)
        {
            CreditCardDetector creditCardDetector = new CreditCardDetector(cardNumber);

            return creditCardDetector.IsValid();
        }

        private bool DateValid(CardDto card)
        {
            return (Enumerable.Range(0, 12).Contains(card.ExpiryMonth)
                    && card.ExpiryMonth >= DateTime.Now.Month
                    && card.ExpiryYear >= DateTime.Now.Year);
        }

        private bool CvvValid(string cvv)
        {
            Regex regex = new Regex(@"^[0-9]{3}$");

            return regex.Match(cvv).Success;
        }
    }
}
