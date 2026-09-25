using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities;
using Wafar.Domain.Entities.Coupons;
using Wafar.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Wafar.Application.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<PartnerDto>> GetAllAsync()
        {
            var repo = _unitOfWork.GetRepository<Partner>();
            var list = await repo.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<PartnerDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Partner>();
            var partner = await repo.GetByIdAsync(id);
            return partner == null ? null : ToDto(partner);
        }

        public async Task<PartnerDto> CreateAsync(CreatePartnerDto dto)
        {
            var repo = _unitOfWork.GetRepository<Partner>();

            var partner = new Partner
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CommissionType = Enum.Parse<CommissionType>(dto.CommissionType),
                DefaultCommissionValue = dto.DefaultCommissionValue,
                IsActive = true
            };

            repo.Add(partner);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(partner);
        }

        public async Task<bool> UpdateAsync(int id, UpdatePartnerDto dto)
        {
            var repo = _unitOfWork.GetRepository<Partner>();
            var partner = await repo.GetByIdAsync(id);
            if (partner == null) return false;

            partner.FullName = dto.FullName;
            partner.Phone = dto.Phone;
            partner.Email = dto.Email;
            partner.Address = dto.Address;
            partner.CommissionType = Enum.Parse<CommissionType>(dto.CommissionType);
            partner.DefaultCommissionValue = dto.DefaultCommissionValue;
            partner.IsActive = dto.IsActive;

            repo.Update(partner);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Partner>();
            var partner = await repo.GetByIdAsync(id);
            if (partner == null) return false;

            repo.Remove(partner);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PartnerCommissionReportDto?> GetCommissionsAsync(int partnerId)
        {
            var partnerRepo = _unitOfWork.GetRepository<Partner>();
            var partner = await partnerRepo.GetByIdAsync(partnerId);
            if (partner == null) return null;

            var commissionRepo = _unitOfWork.GetRepository<Commission>();
            var commissions = await commissionRepo.Query()
                .Where(c => c.PartnerId == partnerId)
                .ToListAsync();

            return new PartnerCommissionReportDto
            {
                PartnerId = partner.Id,
                PartnerName = partner.FullName,
                TotalPending = commissions
                    .Where(c => c.Status == CommissionStatus.Pending)
                    .Sum(c => c.CalculatedAmount),
                TotalPaid = commissions
                    .Where(c => c.Status == CommissionStatus.Paid)
                    .Sum(c => c.CalculatedAmount),
                CommissionsCount = commissions.Count
            };
        }

        private static PartnerDto ToDto(Partner p) => new()
        {
            Id = p.Id,
            FullName = p.FullName,
            Phone = p.Phone,
            Email = p.Email,
            Address = p.Address,
            CommissionType = p.CommissionType.ToString(),
            DefaultCommissionValue = p.DefaultCommissionValue,
            IsActive = p.IsActive
        };
    }
}