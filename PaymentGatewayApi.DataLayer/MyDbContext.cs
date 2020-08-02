using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentGatewayApi.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PaymentGatewayApi.DataLayer
{
    public class MyDbContext : IdentityDbContext<Merchant>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<BankResponse> BankResponses { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PaymentDetails>()
                .HasIndex(p => new { p.Identifier}).IsUnique();
        }
    }
}
