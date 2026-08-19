using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wafar.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto?> LoginAsync(string username, string password);
    }

    public class AuthResultDto
    {
        public string Token { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
