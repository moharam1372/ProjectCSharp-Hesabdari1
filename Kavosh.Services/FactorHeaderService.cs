using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class FactorHeaderService
    {
        private readonly IFactorHeaderRepository _repository;

        public FactorHeaderService(IFactorHeaderRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> GetNextCodeAsync()
        {
            var maxCode = await _repository.GetMaxCodeAsync();
            return maxCode + 1;
        }

        public async Task<FactorHeaderDto> GetFactorByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdWithDetailsAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<Guid> SaveFactorAsync(FactorHeaderDto dto)
        {
            Validate(dto);

            // PriceTotal همیشه سمت سرور از روی خط‌ها محاسبه میشه، نه از چیزی که کلاینت فرستاده
            var calculatedTotal = dto.Details.Sum(d => (long)(d.Count * d.PriceUnit)) - dto.Discount;

            var header = new FactorHeader
            {
                Id = dto.Id,
                Code = dto.Code,
                PersonId = dto.PersonId,
                Type = dto.Type,
                DateFactor = dto.DateFactor,
                Discount = dto.Discount,
                PriceTotal = calculatedTotal
            };

            var details = dto.Details.Select(d => new FactorDetail
            {
                Id = d.Id,
                ProductId = d.ProductId,
                Count = d.Count,
                PriceUnit = d.PriceUnit
            }).ToList();

            var savedId = await _repository.SaveWithDetailsAsync(header, details);
            await _repository.SaveChangesAsync();

            return savedId;
        }

        public async Task DeleteFactorAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity); // Soft Delete روی خود هدر
            await _repository.SaveChangesAsync();
        }

        private static void Validate(FactorHeaderDto dto)
        {
            if (dto.PersonId == Guid.Empty)
                throw new ArgumentException("انتخاب طرف حساب الزامی است");

            if (dto.Details is null || dto.Details.Count == 0)
                throw new ArgumentException("حداقل یک ردیف کالا باید ثبت شود");

            foreach (var d in dto.Details)
            {
                if (d.ProductId == Guid.Empty)
                    throw new ArgumentException("انتخاب کالا برای همه‌ی ردیف‌ها الزامی است");

                if (d.Count <= 0)
                    throw new ArgumentException("تعداد باید بیشتر از صفر باشد");
            }
        }

        private static FactorHeaderDto ToDto(FactorHeader f) => new()
        {
            Id = f.Id,
            Code = f.Code,
            PersonId = f.PersonId,
            PersonName = f.Person?.FullName,
            Type = f.Type,
            DateFactor = f.DateFactor,
            Discount = f.Discount,
            PriceTotal = f.PriceTotal,
            Details = f.FactorDetails.Select(d => new FactorDetailDto
            {
                Id = d.Id,
                ProductId = d.ProductId,
                ProductTitle = d.Product?.Title,
                Count = d.Count,
                PriceUnit = d.PriceUnit
            }).ToList()
        };
    }
}