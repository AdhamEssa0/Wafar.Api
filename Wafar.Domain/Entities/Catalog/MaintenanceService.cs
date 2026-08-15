using Wafar.Domain.Entities.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Catalog
{
    public class MaintenanceService
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
