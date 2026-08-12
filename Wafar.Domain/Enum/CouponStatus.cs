using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Enum
{
    public enum CouponStatus : byte
    {
        Active = 1,
        Used = 2,
        Expired = 3
    }
}
