using Wafar.Domain.Entities;
using Wafar.Domain.Entities.Catalog;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Entities.Rewards;
using Wafar.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Infrastructure.Migrations.Data.Migration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Partner> Partners => Set<Partner>();

        public DbSet<Device> Devices => Set<Device>();
        public DbSet<Accessory> Accessories => Set<Accessory>();
        public DbSet<SparePart> SpareParts => Set<SparePart>();
        public DbSet<MaintenanceService> MaintenanceServices => Set<MaintenanceService>();

        public DbSet<QRCategory> QRCategories => Set<QRCategory>();
        public DbSet<QRCode> QRCodes => Set<QRCode>();

        public DbSet<RewardCategory> RewardCategories => Set<RewardCategory>();
        public DbSet<Reward> Rewards => Set<Reward>();
        public DbSet<QRCodeReward> QRCodeRewards => Set<QRCodeReward>();

        public DbSet<ScanHistory> ScanHistories => Set<ScanHistory>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<CouponUsage> CouponUsages => Set<CouponUsage>();
        public DbSet<Commission> Commissions => Set<Commission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            #region Enum conventions

            // Enum to string/int conventions (اختياري: تخزين كـ string لوضوح القراءة)
            modelBuilder.Entity<QRCode>()
                .Property(x => x.CommissionType)
                .HasConversion<byte?>();

            modelBuilder.Entity<Reward>()
                .Property(x => x.RewardType)
                .HasConversion<byte>();

            modelBuilder.Entity<Coupon>()
                .Property(x => x.Status)
                .HasConversion<byte>();

            modelBuilder.Entity<Commission>()
                .Property(x => x.CommissionType)
                .HasConversion<byte>();

            modelBuilder.Entity<Commission>()
                .Property(x => x.Status)
                .HasConversion<byte>();
            #endregion

            #region Seed Data

            // 1) QR Categories
            modelBuilder.Entity<QRCategory>().HasData(
                new QRCategory { Id = 1, Name = "Devices" },
                new QRCategory { Id = 2, Name = "Maintenance" },
                new QRCategory { Id = 3, Name = "Accessories" },
                new QRCategory { Id = 4, Name = "SpareParts" },
                new QRCategory { Id = 5, Name = "Marketing" },
                new QRCategory { Id = 6, Name = "SocialMedia" }
            );
            // 2) Reward Categories
            modelBuilder.Entity<RewardCategory>().HasData(
                new RewardCategory { Id = 1, Name = "Discounts" },
                new RewardCategory { Id = 2, Name = "FreeItems" },
                new RewardCategory { Id = 3, Name = "FreeServices" }
            );
            // 3) شريك تجريبي
            modelBuilder.Entity<Partner>().HasData(
                new Partner
                {
                    Id = 1,
                    FullName = "Ahmed Marketing",
                    Phone = "01000000000",
                    CommissionType = CommissionType.Percentage,
                    DefaultCommissionValue = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            // 4) QR Code تجريبي فعال (مرتبط بالشريك، مسموح 1000 مسحة)
            modelBuilder.Entity<QRCode>().HasData(
                new QRCode
                {
                    Id = 1,
                    Code = "STORE-MAIN-001",
                    Name = "QR رئيسي - واجهة المحل",
                    Description = "QR تجريبي لأغراض الاختبار",
                    QRCategoryId = 5,
                    PartnerId = 1,
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = new DateTime(2026, 12, 31),
                    IsActive = true,
                    ScanLimit = 1000,
                    CurrentScanCount = 0,
                    CommissionType = CommissionType.Percentage,
                    CommissionValue = 10,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            // 5) مكافآت تجريبية بنسب احتمالية (المجموع = 100)
            modelBuilder.Entity<Reward>().HasData(
                new Reward
                {
                    Id = 1,
                    RewardCategoryId = 1,
                    RewardType = RewardType.DiscountPercentage,
                    RewardName = "خصم 10% على الصيانة",
                    Description = "خصم عند إصلاح أي جهاز",
                    DiscountValue = 10,
                    ProbabilityPercentage = 40,
                    IsActive = true,
                    ExpirationDays = 7,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Reward
                {
                    Id = 2,
                    RewardCategoryId = 2,
                    RewardType = RewardType.FreeScreenProtector,
                    RewardName = "اسكرينة مجانية",
                    Description = "تركيب اسكرينة حماية مجانًا",
                    ProbabilityPercentage = 20,
                    IsActive = true,
                    ExpirationDays = 14,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Reward
                {
                    Id = 3,
                    RewardCategoryId = 3,
                    RewardType = RewardType.FreeDeviceCleaning,
                    RewardName = "تنظيف الجهاز مجانًا",
                    ProbabilityPercentage = 25,
                    IsActive = true,
                    ExpirationDays = 10,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Reward
                {
                    Id = 4,
                    RewardCategoryId = 1,
                    RewardType = RewardType.FixedAmountDiscount,
                    RewardName = "خصم 50 جنيه على أي إكسسوار",
                    DiscountValue = 50,
                    ProbabilityPercentage = 15,
                    IsActive = true,
                    ExpirationDays = 7,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            // 6) ربط المكافآت بالـ QR Code التجريبي (Many-to-Many)
            modelBuilder.Entity<QRCodeReward>().HasData(
                new QRCodeReward { Id = 1, QRCodeId = 1, RewardId = 1 },
                new QRCodeReward { Id = 2, QRCodeId = 1, RewardId = 2 },
                new QRCodeReward { Id = 3, QRCodeId = 1, RewardId = 3 },
                new QRCodeReward { Id = 4, QRCodeId = 1, RewardId = 4 }
            );

            // 7) موظف تجريبي (لتفعيل الكوبونات)
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Staff" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Cashier Test",
                    Username = "cashier1",
                    PasswordHash = "TEMP_HASH_REPLACE_LATER",
                    RoleId = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
            // تصنيفات جديدة يختار منها العميل
            modelBuilder.Entity<RewardCategory>().HasData(
                new RewardCategory { Id = 4, Name = "صيانة" },
                new RewardCategory { Id = 5, Name = "أجهزة" },
                new RewardCategory { Id = 6, Name = "إكسسوارات" }
            );

            // مكافأة واحدة على الأقل لكل تصنيف جديد
            modelBuilder.Entity<Reward>().HasData(
                new Reward { Id = 5, RewardCategoryId = 4, RewardType = RewardType.FreeMaintenanceService, RewardName = "فحص مجاني للجهاز", ProbabilityPercentage = 100, IsActive = true, ExpirationDays = 7, CreatedAt = new DateTime(2026, 1, 1) },
                new Reward { Id = 6, RewardCategoryId = 5, RewardType = RewardType.DiscountPercentage, RewardName = "خصم 15% على الأجهزة", DiscountValue = 15, ProbabilityPercentage = 100, IsActive = true, ExpirationDays = 7, CreatedAt = new DateTime(2026, 1, 1) },
                new Reward { Id = 7, RewardCategoryId = 6, RewardType = RewardType.FreeScreenProtector, RewardName = "اسكرينة حماية مجانية", ProbabilityPercentage = 100, IsActive = true, ExpirationDays = 14, CreatedAt = new DateTime(2026, 1, 1) }
            );

            // ربطها بالـ QR الرئيسي
            modelBuilder.Entity<QRCodeReward>().HasData(
                new QRCodeReward { Id = 5, QRCodeId = 1, RewardId = 5 },
                new QRCodeReward { Id = 6, QRCodeId = 1, RewardId = 6 },
                new QRCodeReward { Id = 7, QRCodeId = 1, RewardId = 7 }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 4,
                    FullName = "Cashier Test",
                    Username = "cashier1",
                    PasswordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",
                    RoleId = 2, // Staff
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new User
                {
                    Id = 5,
                    FullName = "Admin Test",
                    Username = "admin1",
                    PasswordHash = "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=",
                    RoleId = 1, // Admin
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            #endregion

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
        }
    }
}

