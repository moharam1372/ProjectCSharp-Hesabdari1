using Kavosh.DataAccess.Repositories;
using Kavosh.Domain;
using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class FactorHeaderService
    {
        private readonly IFactorHeaderRepository _repository;
        private readonly IRepository<PaymentType> _paymentTypeRepository;   // 👈 جدید

        public FactorHeaderService(IFactorHeaderRepository repository, IRepository<PaymentType> paymentTypeRepository)
        {
            _repository = repository;
            _paymentTypeRepository = paymentTypeRepository;
        }
        public async Task<List<FactorHeaderDto>> GetAllFactorsAsync()
        {
            var factors = await _repository.GetAllWithPersonAsync();
            return factors.Select(ToListDto).ToList();
        }

        // نسخه‌ی سبک برای گرید — بدون Details/HowToPays (که فقط موقع باز کردن تک فاکتور لازمه)
        private static FactorHeaderDto ToListDto(FactorHeader f) => new()
        {
            Id = f.Id,
            Code = f.Code,
            PersonId = f.PersonId,
            PersonName = f.Person?.FullName,
            Type = f.Type,
            DateFactor = f.DateFactor,
            Discount = f.Discount,
            PriceTotal = f.PriceTotal
        };
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
     
            ValidateHowToPays(dto.HowToPays);

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

            var howToPays = dto.HowToPays.Select(p => new HowToPay   // 👈 جدید
            {
                Id = p.Id,
                PaymentTypeId = p.PaymentTypeId,
                Price = p.Price,
                CheckNumber = p.CheckNumber,
                CheckDate = p.CheckDate,
                Settlement = p.Settlement,
                Description = p.Description
            }).ToList();

            var savedId = await _repository.SaveWithDetailsAsync(header, details, howToPays);
            await _repository.SaveChangesAsync();

            return savedId;
        }

        public async Task DeleteFactorAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity is null) return;

            await _repository.Remove(entity);
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

        // 👇 اصلاح‌شده: ساده‌تر، بدون کوئری اضافه به دیتابیس
        private static void ValidateHowToPays(List<HowToPayDto> howToPays)
        {
            if (howToPays is null || howToPays.Count == 0)
                return;

            foreach (var hp in howToPays)
            {
                if (hp.PaymentTypeId == Guid.Empty)
                    throw new ArgumentException("انتخاب نوع پرداخت الزامی است");

                if (hp.Price <= 0)
                    throw new ArgumentException("مبلغ پرداخت باید بیشتر از صفر باشد");

                if (hp.PaymentTypeId == PaymentTypeIds.Check)
                {
                    if (string.IsNullOrWhiteSpace(hp.CheckNumber))
                        throw new ArgumentException("برای پرداخت چکی، شماره چک الزامی است");

                    if (hp.CheckDate is null || hp.CheckDate == default)   // 👈 چک Nullable
                        throw new ArgumentException("برای پرداخت چکی، تاریخ چک الزامی است");
                }
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
            }).ToList(),
            HowToPays = f.HowToPays.Select(p => new HowToPayDto   // 👈 جدید
            {
                Id = p.Id,
                PaymentTypeId = p.PaymentTypeId,
                PaymentTypeTitle = p.PaymentType?.Title,
                Price = p.Price,
                CheckNumber = p.CheckNumber,
                CheckDate = p.CheckDate,
                Settlement = p.Settlement,
                Description = p.Description
            }).ToList()
        };
    }
}