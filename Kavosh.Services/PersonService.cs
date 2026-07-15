using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class PersonService
    {
        private readonly IRepository<Person> _repository;

        public PersonService(IRepository<Person> repository)
        {
            _repository = repository;
        }

        // ============= خواندن =============
        public async Task<List<PersonDto>> GetAllPersonsAsync()
        {
            var persons = await _repository.GetAll();
            return persons.Select(ToDto).ToList();
        }

        public async Task<PersonDto> GetPersonByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? null : ToDto(entity);
        }

        // ============= ذخیره (افزودن + ویرایش با یک متد) =============
        public async Task<Guid> SavePersonAsync(PersonDto dto)
        {
            var (entity, isNew) = await _repository.GetOrNew(dto.Id);

            await ValidateAsync(dto, isNew);

            entity.FullName = dto.FullName;
            entity.Mobile = dto.Mobile;
            entity.CodeMelli = dto.CodeMelli;
            entity.Address = dto.Address;

            if (isNew)
                await _repository.Add(entity);
            else
                await _repository.Update(entity);

            await _repository.SaveChangesAsync();
            return entity.Id;
        }

        // ============= حذف (Soft Delete) =============
        public async Task DeletePersonAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null)
                return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        // ============= کمکی =============
        private async Task ValidateAsync(PersonDto dto, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("نام و نام‌خانوادگی الزامی است");

            if (!string.IsNullOrWhiteSpace(dto.CodeMelli))
            {
                if (dto.CodeMelli.Length != 10 || !dto.CodeMelli.All(char.IsDigit))
                    throw new ArgumentException("کد ملی باید ۱۰ رقم باشد");

                var existing = await _repository.First(p => p.CodeMelli == dto.CodeMelli);
                var isDuplicate = existing is not null && (isNew || existing.Id != dto.Id);

                if (isDuplicate)
                    throw new ArgumentException("این کد ملی قبلاً ثبت شده است");
            }
        }

        private static PersonDto ToDto(Person p) => new()
        {
            Id = p.Id,
            FullName = p.FullName,
            Mobile = p.Mobile,
            CodeMelli = p.CodeMelli,
            Address = p.Address
        };
    }
}