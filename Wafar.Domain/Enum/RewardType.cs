using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Enum
{
    public enum RewardType : byte
    {
        DiscountPercentage = 1,
        FixedAmountDiscount = 2,
        FreeAccessory = 3,
        FreeScreenProtector = 4,
        FreeCase = 5,
        FreeMaintenanceService = 6,
        FreeSparePart = 7,
        FreeDeviceCleaning = 8,
        FreeDeviceCheckup = 9
    }
}
