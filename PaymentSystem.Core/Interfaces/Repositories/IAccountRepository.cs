using PaymentSystem.Core.Models;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdIncludingPaymentsOrderByDate(int id);
    }
}
