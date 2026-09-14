using Kavosh.DataAccess.Repositories;
using Kavosh.Domain.Entities;
using Kavosh.Services.DTOs;

namespace Kavosh.Services
{
    public class CashRegisterService
    {
        private readonly ICashDocumentRepository _documentRepository;
        private readonly IFactorHeaderRepository _factorHeaderRepository;
        private readonly ISettlementRecordRepository _settlementRepository;   // 👈 جدید


        public CashRegisterService(ICashDocumentRepository documentRepository, IFactorHeaderRepository factorHeaderRepository,
            ISettlementRecordRepository settlementRepository)   // 👈 جدید
        {
            _documentRepository = documentRepository;
            _factorHeaderRepository = factorHeaderRepository;
            _settlementRepository = settlementRepository;
        }
        // 👇 جدید — ثبت یک «سند تسویه» با جمع کل فعلیِ سندهای تسویه‌شده (خرید و فروش) و محاسبه‌ی سود
        public async Task<Guid> CreateSettlementRecordAsync(string description)
        {
            var saleDocuments = await _documentRepository.GetByTypeAndSettlementAsync(true, true);
            var purchaseDocuments = await _documentRepository.GetByTypeAndSettlementAsync(false, true);

            if (saleDocuments.Count == 0 && purchaseDocuments.Count == 0)
                throw new ArgumentException("هیچ سند تسویه‌شده‌ای برای ثبت سود وجود ندارد");

            var totalSales = saleDocuments.Sum(d => d.TotalAmount);
            var totalPurchases = purchaseDocuments.Sum(d => d.TotalAmount);

            var allDocIds = saleDocuments.Select(d => d.Id).Concat(purchaseDocuments.Select(d => d.Id));

            var maxCode = await _settlementRepository.GetMaxCodeAsync();

            var record = new SettlementRecord
            {
                Id = Guid.NewGuid(),
                Code = maxCode + 1,
                DateSettlement = DateTime.Now,
                TotalSales = totalSales,
                TotalPurchases = totalPurchases,
                Profit = totalSales - totalPurchases,
                DocumentCount = saleDocuments.Count + purchaseDocuments.Count,
                CashDocumentIds = string.Join(",", allDocIds),
                Description = description
            };

            await _settlementRepository.Add(record);
            await _settlementRepository.SaveChangesAsync();

            return record.Id;
        }

        public async Task<List<SettlementRecordDto>> GetSettlementRecordsAsync()
        {
            var items = await _settlementRepository.GetAllOrderedAsync();
            return items.Select(ToSettlementDto).OrderByDescending(s => s.DateSettlement).ToList();
        }

        public async Task DeleteSettlementRecordAsync(Guid id)
        {
            var entity = await _settlementRepository.GetById(id);
            if (entity is null) return;

            await _settlementRepository.Remove(entity);
            await _settlementRepository.SaveChangesAsync();
        }

        private static SettlementRecordDto ToSettlementDto(SettlementRecord s) => new()
        {
            Id = s.Id,
            Code = s.Code,
            DateSettlement = s.DateSettlement,
            TotalSales = s.TotalSales,
            TotalPurchases = s.TotalPurchases,
            Profit = s.Profit,
            DocumentCount = s.DocumentCount,
            Description = s.Description
        };
        // ============= تب 1 و 2: فاکتورهای سندنخورده =============
        public async Task<List<UndocumentedFactorDto>> GetUndocumentedFactorsAsync(bool type)
        {
            var factors = await _factorHeaderRepository.GetByTypeAsync(type);
            var documentedIds = await _factorHeaderRepository.GetDocumentedFactorIdsAsync();   // 👈 یک کوئری، یک منبع

            return factors
                .Where(f => !documentedIds.Contains(f.Id))
                .Select(f => new UndocumentedFactorDto
                {
                    Id = f.Id,
                    Code = f.Code,
                    PersonName = f.Person?.FullName,
                    DateFactor = f.DateFactor,
                    PriceTotal = f.PriceTotal
                })
                .ToList();
        }

        // ============= ایجاد سند از فاکتورهای انتخاب‌شده =============
        public async Task<Guid> CreateDocumentAsync(bool type, List<Guid> factorIds, string description)
        {
            if (factorIds is null || factorIds.Count == 0)
                throw new ArgumentException("حداقل یک فاکتور باید انتخاب شود");

            var factors = await _factorHeaderRepository.GetByIdsAsync(factorIds);

            if (factors.Count != factorIds.Count)
                throw new ArgumentException("برخی از فاکتورهای انتخاب‌شده معتبر نیستند");

            // 👇 اصلاح شد — استفاده از همون منبع یکپارچه (به‌جای چک تک‌تک با IsFactorDocumentedAsync)
            var documentedIds = await _factorHeaderRepository.GetDocumentedFactorIdsAsync();

            foreach (var f in factors)
            {
                if (f.Type != type)
                    throw new ArgumentException("همه‌ی فاکتورهای انتخاب‌شده باید هم‌نوع (خرید یا فروش) باشند");

                if (documentedIds.Contains(f.Id))
                    throw new ArgumentException($"فاکتور شماره {f.Code} قبلاً در یک سند دیگر ثبت شده است");
            }

            var maxCode = await _documentRepository.GetMaxCodeAsync();

            var document = new CashDocument
            {
                Id = Guid.NewGuid(),
                Code = maxCode + 1,
                Type = type,
                FactorHeaderIds = string.Join(",", factors.Select(f => f.Id)),
                TotalAmount = factors.Sum(f => f.PriceTotal),
                DateDocument = DateTime.Now,
                Description = description
            };

            await _documentRepository.Add(document);
            await _documentRepository.SaveChangesAsync();

            return document.Id;
        }

        // ============= حذف سند (آزادسازی فاکتورها برای ویرایش مجدد) =============
        public async Task DeleteDocumentAsync(Guid documentId)
        {
            var document = await _documentRepository.GetById(documentId);
            if (document is null) return;

            if (document.IsSettled)   // 👈 جدید
                throw new InvalidOperationException("این سند تسویه شده است و قابل حذف نیست.");

            await _documentRepository.Remove(document);
            await _documentRepository.SaveChangesAsync();
        }

        // isSettled: null = همه، true = فقط تسویه‌شده، false = فقط تسویه‌نشده
        public async Task<List<CashDocumentDto>> GetDocumentsAsync(bool type, bool? isSettled = null)
        {
            var documents = await _documentRepository.GetByTypeAndSettlementAsync(type, isSettled);
            return documents.Select(ToDto).ToList();
        }

        // ============= تب 4: گزارش صندوق =============
        public async Task<CashProfitReportDto> GetProfitReportAsync()
        {
            var saleDocuments = await _documentRepository.GetByTypeAndSettlementAsync(true, true);       // 👈 فقط تسویه‌شده
            var purchaseDocuments = await _documentRepository.GetByTypeAndSettlementAsync(false, true);  // 👈 فقط تسویه‌شده

            return new CashProfitReportDto
            {
                TotalSales = saleDocuments.Sum(d => d.TotalAmount),
                TotalPurchases = purchaseDocuments.Sum(d => d.TotalAmount)
            };
        }
        // 👇 جدید — علامت‌گذاری سند به‌عنوان تسویه‌شده (قطعی)
        //public async Task SettleDocumentAsync(Guid documentId)
        public async Task SettleDocumentAsync(Guid[] documentIds)
        {
            //var document = await _documentRepository.GetById(documentId);
            var document = await _documentRepository.Find(f => documentIds.Any(a => a == f.Id) && f.IsSettled == false);
            if (document is null)
                throw new InvalidOperationException("سند یافت نشد");
   

            document.ToList().ForEach(f => f.IsSettled = true);
        
            await _documentRepository.UpdateRange(document.ToList());
            //await _documentRepository.SaveChangesAsync();

            #region New Update

            // true = فروش، false = خرید
            var totalSales = document.Where(w=>w.Type==true).Sum(d => d.TotalAmount);
            //var totalSales = saleDocuments.Sum(d => d.TotalAmount);
            var totalPurchases = document.Where(w => w.Type == false).Sum(d => d.TotalAmount);
            //var totalPurchases = purchaseDocuments.Sum(d => d.TotalAmount);

            //var allDocIds = saleDocuments.Select(d => d.Id).Concat(purchaseDocuments.Select(d => d.Id));

            var maxCode = await _settlementRepository.GetMaxCodeAsync();

            var record = new SettlementRecord
            {
                Id = Guid.NewGuid(),
                Code = maxCode + 1,
                DateSettlement = DateTime.Now,
                TotalSales = totalSales,
                TotalPurchases = totalPurchases,
                Profit = totalSales - totalPurchases,
                DocumentCount = documentIds.Length,
                //DocumentCount = saleDocuments.Count + purchaseDocuments.Count,
                CashDocumentIds = string.Join(",", documentIds),
                //CashDocumentIds = string.Join(",", allDocIds),
                Description = "فعلا ندارد"
            };

            await _settlementRepository.Add(record);
            await _documentRepository.SaveChangesAsync();
            await _settlementRepository.SaveChangesAsync();

            #endregion
        }
    

        private static CashDocumentDto ToDto(CashDocument d) => new()
        {
            Id = d.Id,
            Code = d.Code,
            Type = d.Type,
            DateDocument = d.DateDocument,
            TotalAmount = d.TotalAmount,
            FactorCount = string.IsNullOrWhiteSpace(d.FactorHeaderIds)
                ? 0
                : d.FactorHeaderIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Length,
            Description = d.Description,
            IsSettled = d.IsSettled   // 👈 جدید
        };
    }
}