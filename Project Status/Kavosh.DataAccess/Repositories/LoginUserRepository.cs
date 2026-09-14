using Kavosh.Domain.Entities;
using Kavosh.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Kavosh.DataAccess.Repositories
{
    // public interface IChequeRepository : IRepository<Cheque>
    // نمونه‌ی Repository اختصاصی برای کوئری‌های خاص Customer
    public interface ILoginUserRepository : IRepository<LoginUser>
    {
        Task<Dictionary<bool, string>> Enter(string user, string pass);
        //Task<bool> UpdatePass(string user,  string newPass);
    }

    public class LoginUserRepository : Repository<LoginUser>, ILoginUserRepository
    {
        public LoginUserRepository(AppDbContext context) : base(context) { }

        //public async Task<Customer> GetByPhoneNumberAsync(string phoneNumber)
        //{
        //    return await _dbSet.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        //}

        public async Task<Dictionary<bool, string>> Enter(string user, string pass)
        {
            var getStatus = await _dbSet.FirstOrDefaultAsync(c => c.Username == user && c.Password == pass);

            if (getStatus is null)
            {
                return new Dictionary<bool, string> { { false, "خطا" } };
            } else
                return new Dictionary<bool, string> { { true, getStatus .FullName} };
            //return new Dictionary<bool, string> { { getStatus??true, "بله" } };
            //return null;
        }

        //public async Task<bool> UpdatePass(string user, string newPass)
        //{

        //}
    }
}
