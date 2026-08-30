using Wafar.Application.DTOs;
using Wafar.Application.Interfaces;
using Wafar.Domain.Contracts;
using Wafar.Domain.Entities;
using Wafar.Domain.Entities.Coupons;
using Microsoft.EntityFrameworkCore;

namespace Wafar.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> GetOrCreateByPhoneAsync(GetOrCreateCustomerDto dto)
        {
            var repo = _unitOfWork.GetRepository<Customer>();

            var existing = await repo.SingleOrDefaultAsync(c => c.Phone == dto.Phone);
            if (existing != null)
                return ToDto(existing);

            var customer = new Customer
            {
                Phone = dto.Phone,
                FullName = dto.FullName,
                Email = dto.Email
            };

            repo.Add(customer);
            await _unitOfWork.SaveChangesAsync();

            return ToDto(customer);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Customer>();
            var customer = await repo.GetByIdAsync(id);
            return customer == null ? null : ToDto(customer);
        }

        public async Task<IReadOnlyList<CouponSummaryDto>> GetCouponsAsync(int customerId)
        {
            var couponRepo = _unitOfWork.GetRepository<Coupon>();

            var coupons = await couponRepo.Query()
                .Include(c => c.Reward)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();

            return coupons.Select(c => new CouponSummaryDto
            {
                UniqueCode = c.UniqueCode,
                RewardName = c.Reward.RewardName,
                Status = c.Status.ToString(),
                ExpirationDate = c.ExpirationDate
            }).ToList();
        }

        private static CustomerDto ToDto(Customer c) => new()
        {
            Id = c.Id,
            FullName = c.FullName,
            Phone = c.Phone,
            Email = c.Email
        };
    }
}