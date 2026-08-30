using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
    }

    public class GetOrCreateCustomerDto
    {
        public string Phone { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}