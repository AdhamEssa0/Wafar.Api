using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities;
using Wafar.Infrastructure.Migrations.Data.Migration;

namespace Wafar.Infrastructure.Repositories
{
    public class PartnerRepository : GenericRepository<Partner> , IPartnerRepository
    {
        public PartnerRepository(AppDbContext context) : base(context) { }
    }
}
