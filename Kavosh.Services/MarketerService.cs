using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class MarketerService
    {
        private readonly IMarketerRepository _repository;
        private readonly IFactorHeaderRepository _factorHeaderRepository;

        public MarketerService(IMarketerRepository repository, IFactorHeaderRepository factorHeaderRepository)
        {
            _repository = repository;
            _factorHeaderRepository = factorHeaderRepository;
        }

        public async Task<List<MarketerDto>> GetAllAsync()
        {
            var items = await _repository.GetAll();
            return items.Select(ToDto).ToList();
        }

        public async Task<MarketerDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<Guid> SaveAsync(MarketerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("نام کامل بازاریاب الزامی است");

            var existing = await _repository.GetByFullNameAsync(dto.FullName);
            var isDuplicate = existing is not null && existing.Id != dto.Id;
            if (isDuplicate)
                throw new ArgumentException("این نام  قبلاً ثبت شده است");

            var (entity, isNew) = await _repository.GetOrNew(dto.Id);
            entity.FullName = dto.FullName;
            entity.PhoneNumber = dto.PhoneNumber;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var hasFactors = await _repository.HasFactorsAsync(id);
            if (hasFactors)
                throw new InvalidOperationException("این بازاریاب به یک یا چند فاکتور وصل است و قابل حذف نیست");

            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        // ============= گزارش =============
        public async Task<List<MarketerReportDto>> GetMarketerReportAsync()
        {
            var factors = await _factorHeaderRepository.GetAllWithPersonAndMarketerAsync();

            return factors
                .Where(f => f.MarketerId.HasValue)
                .GroupBy(f => f.MarketerId!.Value)
                .Select(g => new MarketerReportDto
                {
                    MarketerId = g.Key,
                    MarketerFullName = g.First().Marketer?.FullName,
                    PhoneNumber = g.First().Marketer?.PhoneNumber,
                    FactorCount = g.Count(),
                    CustomerCount = g.Select(f => f.PersonId).Distinct().Count(),
                    TotalSales = g.Where(f => f.Type).Sum(f => f.PriceTotal)
                })
                .OrderByDescending(x => x.TotalSales)
                .ToList();
        }

        private static MarketerDto ToDto(Marketer m) => new()
        {
            Id = m.Id,
            FullName = m.FullName,
            PhoneNumber = m.PhoneNumber
        };
    }
}