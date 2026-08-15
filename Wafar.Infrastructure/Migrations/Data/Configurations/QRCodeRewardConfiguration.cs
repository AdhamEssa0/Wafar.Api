using Wafar.Domain.Entities.Rewards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Infrastructure.Data.Configurations
{
    public class QRCodeRewardConfiguration : IEntityTypeConfiguration<QRCodeReward>
    {
        public void Configure(EntityTypeBuilder<QRCodeReward> builder)
        {
            builder.ToTable("QRCodeRewards");
            builder.Property(x => x.ProbabilityOverride).HasColumnType("decimal(5,2)");

            builder.HasOne(x => x.QRCode)
                .WithMany(q => q.QRCodeRewards)
                .HasForeignKey(x => x.QRCodeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Reward)
                .WithMany(r => r.QRCodeRewards)
                .HasForeignKey(x => x.RewardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.QRCodeId, x.RewardId }).IsUnique();
        }
    }

}
