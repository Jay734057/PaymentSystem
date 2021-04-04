using PaymentSystem.Core.Models;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces.Business
{
    public interface IAccountBusiness
    {
        Task<Account> GetAccountByIdIncludingPayments(int id);
    }
}
