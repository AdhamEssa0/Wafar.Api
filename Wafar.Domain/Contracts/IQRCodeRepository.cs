using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Qr;

namespace Wafar.Domain.Contracts
{
    public interface IQRCodeRepository : IGenericRepository<QRCode>
    {
        Task<QRCode?> GetByCodeWithRewardsAsync(string code);
        Task<bool> IsActiveAndWithinLimitsAsync(string code);
        Task IncrementScanCountAsync(int qrCodeId);
        Task<IEnumerable<QRCode>> GetByPartnerAsync(int partnerId);
    }
}
