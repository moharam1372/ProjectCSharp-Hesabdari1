using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        // ============= خواندن =============
        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAll();
            return customers.Select(ToDto).ToList();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.Any(id);
        }

        // ============= افزودن =============
        public async Task<Guid> AddCustomerAsync(CustomerDto dto)
        {
            Validate(dto);

            var entity = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName // فعلاً؛ بعداً از سیستم Login میاد
            };

            // نکته: Id و CreatedAt/IsDeleted داخل Repository.Add ست میشن، نیازی به تنظیم دستی نیست
            await _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return entity.Id;
        }

        // ============= ویرایش =============
        public async Task UpdateCustomerAsync(CustomerDto dto)
        {
            Validate(dto);

            var entity = await _repository.GetById(dto.Id);
            if (entity is null)
                throw new InvalidOperationException("مشتری یافت نشد");

            entity.FullName = dto.FullName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = Environment.UserName;

            // نکته: UpdatedAt به‌صورت خودکار داخل Repository.SaveChangesAsync ست میشه
            await _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        // ============= حذف (Soft Delete) =============
        public async Task DeleteCustomerAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null)
                return;

            await _repository.Remove(entity); // IsDeleted = true, رکورد پاک نمیشه
            await _repository.SaveChangesAsync();
        }

        // ============= کمکی =============
        private static void Validate(CustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("نام مشتری الزامی است");
        }

        private static CustomerDto ToDto(Customer c) => new()
        {
            Id = c.Id,
            FullName = c.FullName,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email
        };
    }
}