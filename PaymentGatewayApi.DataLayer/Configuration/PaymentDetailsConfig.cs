using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.DataLayer.Configuration
{
    class PaymentDetailsConfig : IEntityTypeConfiguration<PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<PaymentDetails> builder)
        {
            builder.ToTable("PaymentDetails");
            builder.HasKey("Id");
            
            builder
                .HasOne(b => b.Merchant)
                .WithMany(a => a.PaymentDetails);

        }
    }
}
