using CodeKhasm.Domain.Entities.Qr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Infrastructure.Data.Configurations
{
    public class QRCodeConfiguration : IEntityTypeConfiguration<QRCode>
    {
        public void Configure(EntityTypeBuilder<QRCode> builder)
        {
            builder.ToTable("QRCodes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CommissionValue).HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.QRCategory)
                .WithMany(c => c.QRCodes)
                .HasForeignKey(x => x.QRCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Partner)
                .WithMany(p => p.QRCodes)
                .HasForeignKey(x => x.PartnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Branch)
                .WithMany(b => b.QRCodes)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.IsActive, x.StartDate, x.EndDate });
        }
    }

}
