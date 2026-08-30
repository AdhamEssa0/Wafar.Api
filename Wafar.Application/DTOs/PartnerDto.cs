using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string CommissionType { get; set; } = null!;
        public decimal DefaultCommissionValue { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreatePartnerDto
    {
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string CommissionType { get; set; } = null!;
        public decimal DefaultCommissionValue { get; set; }
    }

    public class UpdatePartnerDto
    {
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string CommissionType { get; set; } = null!;
        public decimal DefaultCommissionValue { get; set; }
        public bool IsActive { get; set; }
    }

    // تقرير مبسط لعمولات الشريك
    public class PartnerCommissionReportDto
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; } = null!;
        public decimal TotalPending { get; set; }
        public decimal TotalPaid { get; set; }
        public int CommissionsCount { get; set; }
    }
}
