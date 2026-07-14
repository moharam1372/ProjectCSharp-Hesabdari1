# YourApp — راهنمای راه‌اندازی

این Solution شامل معماری لایه‌بندی‌شده (Layered Architecture) با WinForms + EF Core + DevExpress است.

## ساختار پروژه‌ها

| پروژه | مسئولیت |
|---|---|
| `YourApp.Domain` | Entities و Interfaceهای پایه (بدون وابستگی به EF/UI) |
| `YourApp.DataAccess` | `AppDbContext`، Repositoryها، Migrationها (EF Core) |
| `YourApp.Services` | منطق تجاری و DTOها |
| `YourApp.UI` | فرم‌های WinForms + DevExpress |

نمونه‌ی کامل برای موجودیت `Customer` در هر ۴ لایه پیاده شده تا الگوی تکرار برای ۱۴ جدول باقی‌مانده مشخص باشد.

## پیش‌نیازها

1. **.NET 8 SDK** — https://dotnet.microsoft.com/download
2. **Visual Studio 2022** (17.8+) با workload های:
   - .NET Desktop Development
   - (برای WinForms Designer)
3. **SQL Server** (Express یا LocalDB کافیست)
4. **لایسنس DevExpress** و دسترسی به NuGet Feed اختصاصی DevExpress

## نصب DevExpress (مرحله‌ای که باید خودتان انجام دهید)

بسته‌های DevExpress به‌خاطر لایسنس در این Solution *اضافه نشده‌اند*. بعد از باز کردن پروژه:

1. در Visual Studio: `Tools > NuGet Package Manager > Package Sources`
2. یک Source جدید اضافه کنید: `https://nuget.devexpress.com/{your-feed-key}/api`
   (feed key از اکانت DevExpress شما قابل دریافت است)
3. روی پروژه‌ی `YourApp.UI` راست‌کلیک و پکیج‌های زیر را نصب کنید (نسخه متناسب با لایسنستان):
   - `DevExpress.Win.Grid`
   - `DevExpress.Win.Design`
   - `DevExpress.Win.Navigation` (در صورت نیاز به منو/ریبون)

> پس از نصب، فایل `Forms/CustomerForm.Designer.cs` را در Visual Studio Designer باز کنید — چون این فایل به‌صورت دستی نوشته شده، توصیه می‌شود کنترل‌ها را یک‌بار در Designer با drag & drop بازسازی کنید تا طراحی بصری هم قابل ویرایش شود.

## تنظیم Connection String

فایل `YourApp.UI/appsettings.json` را باز کرده و مقدار `DefaultConnection` را با سرور خودتان تطبیق دهید:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=YourAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## ساخت Migration اولیه و دیتابیس

از **Package Manager Console** در Visual Studio (با انتخاب `YourApp.DataAccess` به‌عنوان Default project):

```powershell
Add-Migration InitialCreate -Project YourApp.DataAccess -StartupProject YourApp.UI
Update-Database -Project YourApp.DataAccess -StartupProject YourApp.UI
```

یا با CLI:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project YourApp.DataAccess --startup-project YourApp.UI
dotnet ef database update --project YourApp.DataAccess --startup-project YourApp.UI
```

## اجرای پروژه

`YourApp.UI` را به‌عنوان Startup Project تنظیم کنید و F5 بزنید.

## الگوی افزودن یک جدول جدید (تکرار برای ۱۴ جدول دیگر)

برای هر جدول جدید (مثلاً `Product`) این مراحل را تکرار کنید:

1. **Domain**: `Entities/Product.cs` بسازید (مثل الگوی `Customer.cs`)
2. **DataAccess**:
   - `DbSet<Product> Products` را به `AppDbContext.cs` اضافه کنید
   - در `OnModelCreating` تنظیمات مربوطه را بنویسید
   - در صورت نیاز به کوئری خاص، `ProductRepository.cs` مشابه `CustomerRepository.cs` بسازید
3. **Services**: `ProductDto.cs` و `ProductService.cs` مشابه الگوی Customer
4. **UI**: `ProductForm.cs` + Designer، و ثبت آن در `Program.cs` (`services.AddTransient<ProductForm>();`)
5. یک Migration جدید بسازید: `Add-Migration AddProductTable ...`

## نکات معماری مهم

- فرم‌ها **هرگز** مستقیم `DbContext` یا `Repository` را نمی‌بینند — فقط از طریق Service layer.
- `RowVersion` (Optimistic Concurrency) در Entityها از همین الان جاسازی شده تا وقتی چندکاربره شدید، تداخل نوشتن هم‌زمان مدیریت شود.
- `CreatedBy` / `ModifiedBy` فعلاً از `Environment.UserName` پر می‌شود؛ وقتی سیستم Login اضافه شد، این مقدار را از کاربر لاگین‌شده بگیرید.
- برای گزارش‌گیری از **DevExpress XtraReports** استفاده کنید که مستقیماً با خروجی Serviceها (DTOها) کار می‌کند.
