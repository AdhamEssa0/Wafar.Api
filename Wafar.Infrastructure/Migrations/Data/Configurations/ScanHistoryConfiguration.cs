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
    public class ScanHistoryConfiguration : IEntityTypeConfiguration<ScanHistory>
    {
        public void Configure(EntityTypeBuilder<ScanHistory> builder)
        {
            builder.ToTable("ScanHistories");

            builder.HasOne(x => x.QRCode)
                .WithMany(q => q.ScanHistories)
                .HasForeignKey(x => x.QRCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.ScanHistories)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.QRCodeId, x.ScanDate });
        }
    }

}
