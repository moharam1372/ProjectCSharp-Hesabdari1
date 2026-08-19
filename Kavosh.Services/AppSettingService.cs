using Kavosh.Domain.Constants;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class AppSettingService
    {
        private readonly IRepository<AppSetting> _repository;

        public AppSettingService(IRepository<AppSetting> repository)
        {
            _repository = repository;
        }

        public async Task<AppSettingDto> GetAsync()
        {
            var entity = await _repository.GetById(AppSettingIds.Default);
            return entity is null ? new AppSettingDto() : ToDto(entity);
        }

        public async Task SaveAsync(AppSettingDto dto)
        {
            var (entity, isNew) = await _repository.GetOrNew(AppSettingIds.Default);
            entity.PreventNegativeInventory = dto.PreventNegativeInventory;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
        }

        private static AppSettingDto ToDto(AppSetting s) => new()
        {
            PreventNegativeInventory = s.PreventNegativeInventory
        };
    }
}