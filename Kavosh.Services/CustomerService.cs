using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;

namespace Kavosh.Services
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }




        //public async Task<List<CustomerDto>> GetAllCustomersAsync()
        //{
        //    var customers = await _repository.GetAllAsync();
        //    return customers.Select(c => new CustomerDto
        //    {
        //        Id = c.Id,
        //        FullName = c.FullName,
        //        PhoneNumber = c.PhoneNumber,
        //        Email = c.Email
        //    }).ToList();
        //}

        //public async Task AddCustomerAsync(CustomerDto dto)
        //{
        //    // اینجا validation و business rule ها قرار می‌گیرن
        //    if (string.IsNullOrWhiteSpace(dto.FullName))
        //        throw new ArgumentException("نام مشتری الزامی است");

        //    var entity = new Customer
        //    {
        //        FullName = dto.FullName,
        //        PhoneNumber = dto.PhoneNumber,
        //        Email = dto.Email,
        //        CreatedDate = DateTime.Now,
        //        CreatedBy = Environment.UserName // فعلاً؛ بعداً از سیستم Login میاد
        //    };

        //    await _repository.AddAsync(entity);
        //    await _repository.SaveChangesAsync();
        //}

        //public async Task UpdateCustomerAsync(CustomerDto dto)
        //{
        //    var entity = await _repository.GetByIdAsync(dto.Id);
        //    if (entity == null)
        //        throw new InvalidOperationException("مشتری یافت نشد");

        //    entity.FullName = dto.FullName;
        //    entity.PhoneNumber = dto.PhoneNumber;
        //    entity.Email = dto.Email;
        //    entity.ModifiedDate = DateTime.Now;
        //    entity.ModifiedBy = Environment.UserName;

        //    _repository.Update(entity);
        //    await _repository.SaveChangesAsync();
        //}

        //public async Task DeleteCustomerAsync(int id)
        //{
        //    var entity = await _repository.GetByIdAsync(id);
        //    if (entity == null)
        //        return;

        //    _repository.Delete(entity);
        //    await _repository.SaveChangesAsync();
        //}
    }
}
