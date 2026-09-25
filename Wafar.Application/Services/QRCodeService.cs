using Microsoft.EntityFrameworkCore;
using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities.Qr;
using Wafar.Domain.Entities.Rewards;

namespace Wafar.Application.Services
{
    public class QRCodeService : IQRCodeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QRCodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<QRCodeDto>> GetAllAsync()
        {
            var repo = _unitOfWork.GetRepository<QRCode>();
            var list = await repo.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<QRCodeDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<QRCode>();
            var qr = await repo.GetByIdAsync(id);
            return qr == null ? null : ToDto(qr);
        }

        public async Task<IReadOnlyList<QRCodeRewardDto>> GetRewardsAsync(int qrCodeId)
        {
            var qrRewardRepo = _unitOfWork.GetRepository<QRCodeReward>();

            var qrRewards = await qrRewardRepo.Query()
                .Where(x => x.QRCodeId == qrCodeId)
                .Select(x => new QRCodeRewardDto
                {
                    RewardId = x.RewardId,
                    ProbabilityOverride = x.ProbabilityOverride
                })
                .ToListAsync();

            return qrRewards;
        }

        public async Task<bool> UpdateRewardsAsync(
            int qrCodeId,
            IReadOnlyList<QRCodeRewardDto> rewards)
        {
            var qrRepo = _unitOfWork.GetRepository<QRCode>();
            var qr = await qrRepo.GetByIdAsync(qrCodeId);

            if (qr == null)
                return false;

            var qrRewardRepo = _unitOfWork.GetRepository<QRCodeReward>();

            var existingRewards = await qrRewardRepo.Query()
                .Where(x => x.QRCodeId == qrCodeId)
                .ToListAsync();

            foreach (var existing in existingRewards)
            {
                qrRewardRepo.Remove(existing);
            }

            foreach (var reward in rewards)
            {
                qrRewardRepo.Add(new QRCodeReward
                {
                    QRCodeId = qrCodeId,
                    RewardId = reward.RewardId,
                    ProbabilityOverride = reward.ProbabilityOverride
                });
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<QRCodeDto> CreateAsync(CreateQRCodeDto dto)
        {
            var repo = _unitOfWork.GetRepository<QRCode>();

            var qr = new QRCode
            {
                Code = dto.Code,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ScanLimit = dto.ScanLimit,
                QRCategoryId = dto.QRCategoryId,
                PartnerId = dto.PartnerId,
                IsActive = true,
                CurrentScanCount = 0
            };

            repo.Add(qr);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(qr);
        }

        public async Task<bool> UpdateAsync(int id, UpdateQRCodeDto dto)
        {
            var repo = _unitOfWork.GetRepository<QRCode>();
            var qr = await repo.GetByIdAsync(id);
            if (qr == null) return false;

            qr.Name = dto.Name;
            qr.Description = dto.Description;
            qr.StartDate = dto.StartDate;
            qr.EndDate = dto.EndDate;
            qr.IsActive = dto.IsActive;
            qr.ScanLimit = dto.ScanLimit;
            qr.QRCategoryId = dto.QRCategoryId;
            qr.PartnerId = dto.PartnerId;

            repo.Update(qr);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<QRCode>();
            var qr = await repo.GetByIdAsync(id);
            if (qr == null) return false;

            repo.Remove(qr);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<QRCode>();
            var qr = await repo.GetByIdAsync(id);
            if (qr == null) return false;

            qr.IsActive = !qr.IsActive;
            repo.Update(qr);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private static QRCodeDto ToDto(QRCode q) => new()
        {
            Id = q.Id,
            Code = q.Code,
            Name = q.Name,
            Description = q.Description,
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            IsActive = q.IsActive,
            ScanLimit = q.ScanLimit,
            CurrentScanCount = q.CurrentScanCount,
            QRCategoryId = q.QRCategoryId,
            PartnerId = q.PartnerId
        };
    }
}