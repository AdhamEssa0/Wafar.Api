using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class QRCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int ScanLimit { get; set; }
        public int CurrentScanCount { get; set; }
        public int QRCategoryId { get; set; }
        public int? PartnerId { get; set; }
    }

    public class CreateQRCodeDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScanLimit { get; set; }
        public int QRCategoryId { get; set; }
        public int? PartnerId { get; set; }
    }

    public class UpdateQRCodeDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int ScanLimit { get; set; }
        public int QRCategoryId { get; set; }
        public int? PartnerId { get; set; }
    }
}
