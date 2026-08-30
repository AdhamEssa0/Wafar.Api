using Wafar.Application.Services;

namespace Wafar.Api.BackgroundJobs
{
    public class CouponExpiryWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CouponExpiryWorker> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(1);

        public CouponExpiryWorker(
            IServiceProvider serviceProvider,
            ILogger<CouponExpiryWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // بنعمل Scope جديد في كل مرة عشان ناخد نسخة صح من IUnitOfWork (Scoped)
                    using var scope = _serviceProvider.CreateScope();
                    var expiryService = scope.ServiceProvider
                        .GetRequiredService<CouponExpiryService>();

                    var expiredCount = await expiryService.ExpireOverdueCouponsAsync();

                    if (expiredCount > 0)
                        _logger.LogInformation("تم إنهاء صلاحية {Count} كوبون منتهي.", expiredCount);
                }
                catch (Exception ex)
                {
                    // مهم جدًا: نمسك أي استثناء هنا حتى لا يقفل السيرفر كله
                    // (زي مشكلة الـ Login failed اللي حصلت زمان وقفلت السيرفر كله)
                    _logger.LogError(ex, "حصل خطأ أثناء فحص الكوبونات المنتهية.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}