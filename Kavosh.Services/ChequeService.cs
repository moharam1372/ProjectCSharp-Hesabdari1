using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Enums;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class ChequeService
    {
        private readonly IChequeRepository _repository;

        public ChequeService(IChequeRepository repository)
        {
            _repository = repository;
        }
        public async Task<(long TotalAmount, int Count)> GetPendingSummaryAsync()
        {
            return await _repository.GetPendingSummaryAsync();
        }
        public async Task<List<ChequeDto>> GetAllAsync()
        {
            var items = await _repository.GetAllWithPersonAsync();
            return items.Select(ToDto).ToList();
        }

        /// <summary>
        /// چک‌هایی که سررسیدشون طی «days» روز آینده است (پیش‌فرض ۲ روز طبق مستندات پروژه)
        /// </summary>
        public async Task<List<ChequeDto>> GetUpcomingAsync(int days = 2)
        {
            var items = await _repository.GetUpcomingAsync(days);
            return items.Select(ToDto).ToList();
        }

        // فراخوانی خودکار از FactorHeaderService وقتی یک HowToPay از نوع «چک» ثبت/ویرایش میشه
        public async Task CreateOrUpdateFromHowToPayAsync(Guid howToPayId, Guid personId, string chequeNumber,
            DateTime? dueDate, long price, bool isReceived)
        {
            if (!dueDate.HasValue || string.IsNullOrWhiteSpace(chequeNumber))
                return;

            var existing = await _repository.GetByHowToPayIdAsync(howToPayId);

            if (existing is not null)
            {
                existing.ChequeNumber = chequeNumber;
                existing.DueDate = dueDate.Value;
                existing.Price = price;
                existing.PersonId = personId;
                existing.IsReceived = isReceived;
                await _repository.Update(existing);
            }
            else
            {
                var entity = new Cheque
                {
                    Id = Guid.NewGuid(),
                    ChequeNumber = chequeNumber,
                    DueDate = dueDate.Value,
                    Price = price,
                    PersonId = personId,
                    IsReceived = isReceived,
                    Status = ChequeStatus.Pending,
                    HowToPayId = howToPayId,
                    Description = isReceived ? "چک دریافتی بابت فاکتور فروش" : "چک صادرشده بابت فاکتور خرید"
                };
                await _repository.Add(entity);
            }

            await _repository.SaveChangesAsync();
        }

        // فراخوانی از DefinitiveAccountService برای ثبت دستی چک (دریافت/پرداخت مستقل از فاکتور)
        public async Task CreateFromManualTransactionAsync(Guid definitiveAccountId, Guid personId,
            string chequeNumber, DateTime dueDate, long price, bool isReceived, string description)
        {
            if (string.IsNullOrWhiteSpace(chequeNumber))
                return;

            var entity = new Cheque
            {
                Id = Guid.NewGuid(),
                ChequeNumber = chequeNumber,
                DueDate = dueDate,
                Price = price,
                PersonId = personId,
                IsReceived = isReceived,
                Status = ChequeStatus.Pending,
                DefinitiveAccountId = definitiveAccountId,
                Description = description
            };

            await _repository.Add(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task SetStatusAsync(Guid id, ChequeStatus status)
        {
            var entity = await _repository.GetById(id);
            if (entity is null) return;

            entity.Status = status;
            await _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
            await _repository.SaveChangesAsync();
        }

        private static ChequeDto ToDto(Cheque c) => new()
        {
            Id = c.Id,
            ChequeNumber = c.ChequeNumber,
            DueDate = c.DueDate,
            Price = c.Price,
            PersonId = c.PersonId,
            PersonName = c.Person?.FullName,
            IsReceived = c.IsReceived,
            Status = c.Status,
            Description = c.Description
        };
    }
}