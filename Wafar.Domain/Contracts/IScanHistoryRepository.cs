using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities.Coupons;

namespace Wafar.Domain.Contracts
{
    public interface IScanHistoryRepository : IGenericRepository<ScanHistory>
    {
        Task<int> CountByCodeAsync(int qrCodeId);
        Task<IEnumerable<ScanHistory>> GetByCustomerAsync(int customerId);
    }
}
