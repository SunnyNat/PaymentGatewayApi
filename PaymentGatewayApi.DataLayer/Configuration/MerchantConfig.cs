using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PaymentGatewayApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayApi.DataLayer.Configuration
{
    class MerchantConfig : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("Merchants");
            builder.HasKey(b => b.Id);

        }
    }
}
