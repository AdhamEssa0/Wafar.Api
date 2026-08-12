using CodeKhasm.Domain.Entities.Coupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Infrastructure.Data.Configurations
{
    public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
    {
        public void Configure(EntityTypeBuilder<Commission> builder)
        {
            builder.ToTable("Commissions");
            builder.Property(x => x.CommissionValue).HasColumnType("decimal(10,2)");
            builder.Property(x => x.CalculatedAmount).HasColumnType("decimal(10,2)");

            // One-to-One: Commission -> Coupon
            builder.HasOne(x => x.Coupon)
                .WithOne(c => c.Commission)
                .HasForeignKey<Commission>(x => x.CouponId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.CouponId).IsUnique();

            builder.HasOne(x => x.Partner)
                .WithMany(p => p.Commissions)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.QRCode)
                .WithMany(q => q.Commissions)
                .HasForeignKey(x => x.QRCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.PartnerId, x.Status });
        }
    }
}
