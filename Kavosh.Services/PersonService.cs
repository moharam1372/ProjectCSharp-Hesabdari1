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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.Any(id);
        }

        public async Task<PersonDto> GetByCodeMelliAsync(string codeMelli)
        {
            var entity = await _repository.First(p => p.CodeMelli == codeMelli);
            return entity is null ? null : ToDto(entity);
        }

        // ============= افزودن =============
        public async Task<Guid> AddPersonAsync(PersonDto dto)
        {
            await ValidateAsync(dto, isNew: true);

            var entity = new Person
            {
                FullName = dto.FullName,
                Mobile = dto.Mobile,
                CodeMelli = dto.CodeMelli,
                Address = dto.Address
            };

            // Id و CreatedAt/IsDeleted داخل Repository.Add ست میشن
            await _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return entity.Id;
        }

        // ============= ویرایش =============
        public async Task UpdatePersonAsync(PersonDto dto)
        {
            await ValidateAsync(dto, isNew: false);

            var entity = await _repository.GetById(dto.Id);
            if (entity is null)
                throw new InvalidOperationException("شخص یافت نشد");

            entity.FullName = dto.FullName;
            entity.Mobile = dto.Mobile;
            entity.CodeMelli = dto.CodeMelli;
            entity.Address = dto.Address;

            // UpdatedAt خودکار داخل SaveChangesAsync ست میشه
            await _repository.Update(entity);
            await _repository.SaveChangesAsync();
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

                // جلوگیری از تکراری‌بودن کد ملی
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