using PaymentSystem.Core.Interfaces.Business;
using PaymentSystem.Core.Interfaces.Repositories;
using PaymentSystem.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentSystem.Business
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _accountRepository;

        public AccountBusiness(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<Account> GetAccountByIdIncludingPayments(int id)
        {
            return await _accountRepository.GetAccountByIdIncludingPaymentsOrderByDate(id);
        }
    }
}
