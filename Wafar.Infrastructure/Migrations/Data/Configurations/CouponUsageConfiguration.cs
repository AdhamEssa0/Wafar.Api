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
    public class CouponUsageConfiguration : IEntityTypeConfiguration<CouponUsage>
    {
        public void Configure(EntityTypeBuilder<CouponUsage> builder)
        {
            builder.ToTable("CouponUsages");

            // One-to-One: CouponUsage -> Coupon
            builder.HasOne(x => x.Coupon)
                .WithOne(c => c.CouponUsage)
                .HasForeignKey<CouponUsage>(x => x.CouponId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.CouponId).IsUnique();

            builder.HasOne(x => x.UsedByUser)
                .WithMany(u => u.CouponUsages)
                .HasForeignKey(x => x.UsedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Branch)
                .WithMany(b => b.CouponUsages)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
