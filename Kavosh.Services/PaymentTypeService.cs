// PaymentTypeService.cs
using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class PaymentTypeService
    {
        private readonly IRepository<PaymentType> _repository;

        public PaymentTypeService(IRepository<PaymentType> repository)
        {
            _repository = repository;
        }

        public async Task<List<PaymentTypeDto>> GetAllAsync()
        {
            var items = await _repository.GetAll();
            return items.Select(ToDto).ToList();
        }

        private static PaymentTypeDto ToDto(PaymentType p) => new()
        {
            Id = p.Id,
            Title = p.Title
        };
    }
}