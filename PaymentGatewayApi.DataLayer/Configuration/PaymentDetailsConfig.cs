using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGatewayApi.Models;

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
