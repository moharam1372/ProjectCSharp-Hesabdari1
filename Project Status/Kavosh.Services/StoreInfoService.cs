using Kavosh.Domain.Constants;
using Kavosh.Domain.Interfaces;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class StoreInfoService
    {
        private readonly IRepository<StoreInfo> _repository;

        public StoreInfoService(IRepository<StoreInfo> repository)
        {
            _repository = repository;
        }

        public async Task<StoreInfoDto> GetAsync()
        {
            var entity = await _repository.GetById(StoreInfoIds.Default);
            return entity is null ? null : ToDto(entity);
        }

        public async Task SaveAsync(StoreInfoDto dto)
        {
            var (entity, isNew) = await _repository.GetOrNew(StoreInfoIds.Default);

            entity.StoreName = dto.StoreName;
            entity.Address = dto.Address;
            entity.Phone = dto.Phone;
            entity.BankName = dto.BankName;
            entity.AccountHolderName = dto.AccountHolderName;
            entity.CardNumber = dto.CardNumber;
            entity.ShabaNumber = dto.ShabaNumber;
            entity.TaxPercent = dto.TaxPercent;
            entity.Mohr = dto.Mohr;
            entity.Logo = dto.Logo;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
        }

        private static StoreInfoDto ToDto(StoreInfo s) => new()
        {
            StoreName = s.StoreName,
            Address = s.Address,
            Phone = s.Phone,
            BankName = s.BankName,
            AccountHolderName = s.AccountHolderName,
            CardNumber = s.CardNumber,
            ShabaNumber = s.ShabaNumber,
            TaxPercent = s.TaxPercent,
            Logo = s.Logo,
            Mohr = s.Mohr
        };
    }
}