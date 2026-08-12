using CodeKhasm.Domain.Entities.Rewards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Infrastructure.Data.Configurations
{
    public class RewardConfiguration : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder.ToTable("Rewards");
            builder.Property(x => x.RewardName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.ProbabilityPercentage).HasColumnType("decimal(5,2)");
            builder.Property(x => x.DiscountValue).HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.RewardCategory)
                .WithMany(rc => rc.Rewards)
                .HasForeignKey(x => x.RewardCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Device).WithMany(d => d.Rewards)
                .HasForeignKey(x => x.DeviceId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Accessory).WithMany(a => a.Rewards)
                .HasForeignKey(x => x.AccessoryId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.SparePart).WithMany(s => s.Rewards)
                .HasForeignKey(x => x.SparePartId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.MaintenanceService).WithMany(m => m.Rewards)
                .HasForeignKey(x => x.MaintenanceServiceId).OnDelete(DeleteBehavior.SetNull);
        }
    }

}
