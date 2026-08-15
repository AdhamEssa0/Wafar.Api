using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Qr;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class QRCodeRepository : GenericRepository<QRCode>, IQRCodeRepository
    {
        public QRCodeRepository(AppDbContext context) : base(context) { }

        public async Task<QRCode?> GetByCodeWithRewardsAsync(string code)
        {
            return await _context.QRCodes
                .Include(q => q.QRCodeRewards)
                    .ThenInclude(qr => qr.Reward)
                .Include(q => q.Partner)
                .FirstOrDefaultAsync(q => q.Code == code);
        }

        public async Task<IEnumerable<QRCode>> GetByPartnerAsync(int partnerId)
        {
            return await _context.QRCodes
                .Where(q => q.PartnerId == partnerId)
                .ToListAsync();
        }

        public async Task IncrementScanCountAsync(int qrCodeId)
        {
            var qr = await _context.QRCodes.FindAsync(qrCodeId);
            if (qr is null) return;

            qr.CurrentScanCount++;
            if (qr.ScanLimit > 0 && qr.CurrentScanCount >= qr.ScanLimit)
                qr.IsActive = false;
        }

        public async Task<bool> IsActiveAndWithinLimitsAsync(string code)
        {
            var qr = await _context.QRCodes.AsNoTracking()
                .FirstOrDefaultAsync(q => q.Code == code);

            if (qr is null) return false;

            var now = DateTime.UtcNow;
            var withinDate = now >= qr.StartDate && now <= qr.EndDate;
            var withinLimit = qr.ScanLimit == 0 || qr.CurrentScanCount < qr.ScanLimit;

            return qr.IsActive && withinDate && withinLimit;
        }
    }
}
