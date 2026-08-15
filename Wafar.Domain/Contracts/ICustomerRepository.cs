using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Domain.Commen;
using Wafar.Domain.Entities;

namespace Wafar.Domain.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetByPhoneAsync(string phone);
    }
}
