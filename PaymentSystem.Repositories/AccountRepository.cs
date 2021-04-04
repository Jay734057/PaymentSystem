using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Interfaces.Repositories;
using PaymentSystem.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PaymentSystemContext _context;

        public AccountRepository(PaymentSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Account> GetAccountByIdIncludingPaymentsOrderByDate(int id)
        {
            var account = await _context.Accounts.AsNoTracking().Include(e => e.Payments).SingleOrDefaultAsync(e => e.Id == id);
            //null check, if account exists, sort payments by date
            if (account != null)
            {
                account.Payments = account.Payments.OrderByDescending(e => e.Date).ToList();

            }
            return account;
        }
    }
}
