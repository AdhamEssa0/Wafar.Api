using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Domain.Entities.Rewards
{
    public class RewardCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}
