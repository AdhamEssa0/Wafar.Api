using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Qr
{
    public class QRCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Devices, Maintenance, Accessories, SpareParts, Marketing, SocialMedia

        public ICollection<QRCode> QRCodes { get; set; } = new List<QRCode>();
    }
}
