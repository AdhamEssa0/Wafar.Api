using CodeKhasm.Domain.Entities.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKhasm.Domain.Entities.Catalog
{
    public class SparePart
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? CompatibleModel { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
