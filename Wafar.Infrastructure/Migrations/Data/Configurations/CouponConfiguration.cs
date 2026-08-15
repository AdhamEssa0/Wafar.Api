using Wafar.Domain.Entities.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Infrastructure.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.Property(x => x.UniqueCode).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.UniqueCode).IsUnique();

            // One-to-One: Coupon -> ScanHistory
            builder.HasOne(x => x.ScanHistory)
                .WithOne(s => s.Coupon)
                .HasForeignKey<Coupon>(x => x.ScanHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ScanHistoryId).IsUnique();

            builder.HasOne(x => x.Reward)
                .WithMany(r => r.Coupons)
                .HasForeignKey(x => x.RewardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Coupons)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.Status, x.ExpirationDate });
        }
    }
}
