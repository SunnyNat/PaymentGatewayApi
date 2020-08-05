using Microsoft.EntityFrameworkCore;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayApi.DataLayer
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyDbContext myDbContext;

        public PaymentRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<PaymentDetails> GetPaymentDetails(string paymentIdentifier, Merchant currentUser)
        {
            return await myDbContext.PaymentDetails
                .Include(p => p.Card)
                .Where(p => p.Identifier == paymentIdentifier && p.Merchant == currentUser).FirstOrDefaultAsync();
        }

        public async Task PostPaymentDetails(PaymentDetails paymentDetails)
        {
            await myDbContext.PaymentDetails.AddAsync(paymentDetails);
        }
    }
}
