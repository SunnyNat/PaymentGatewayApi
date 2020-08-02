using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.DataLayer
{
    public class BankResponseConfig : IEntityTypeConfiguration<BankResponse>
    {
        public void Configure(EntityTypeBuilder<BankResponse> builder)
        {
            builder.ToTable("BankResponses");
            builder.HasKey(b => b.Id);
        }
    }
    
}
