using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafar.Application.DTOs;

namespace Wafar.Application.Interfaces
{
    public interface IQRCodeService
    {
        Task<IReadOnlyList<QRCodeDto>> GetAllAsync();
        Task<QRCodeDto?> GetByIdAsync(int id);
        Task<QRCodeDto> CreateAsync(CreateQRCodeDto dto);
        Task<bool> UpdateAsync(int id, UpdateQRCodeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);
    }
}
