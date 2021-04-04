using NSubstitute;
using PaymentSystem.Business;
using PaymentSystem.Core.Interfaces.Business;
using PaymentSystem.Core.Interfaces.Repositories;
using PaymentSystem.Core.Models;
using System.Threading.Tasks;
using Xunit;

namespace PaymentSystem.UnitTests
{
    public class AccountBusinessTests
    {
        private readonly IAccountBusiness _accountBusiness;
        public AccountBusinessTests()
        {
            var accountRepository = Substitute.For<IAccountRepository>();
            accountRepository.GetAccountByIdIncludingPaymentsOrderByDate(1)
                .Returns(
                Task.FromResult(new Account
                {
                    Id = 1,
                    Balance = 1000,
                    Payments = new[]
                    {
                        new Payment
                        {
                            Date = new System.DateTime(2020, 11, 12),
                            Amount = 500,
                            Status = PaymentStatus.Complete,
                            Message = "Payment completed.",
                            AccountId = 1
                        },
                        new Payment
                        {
                            Date = new System.DateTime(2021, 1, 20),
                            Amount = 10,
                            Status = PaymentStatus.Cancelled,
                            Message = "Payment cancelled as requested.",
                            AccountId = 1
                        },
                        new Payment
                        {
                            Date = new System.DateTime(2019, 8, 5),
                            Amount = 300,
                            Status = PaymentStatus.Rejected,
                            Message = "Payment rejected as insufficient fund.",
                            AccountId = 1
                        }
                    }
                }));

            accountRepository.GetAccountByIdIncludingPaymentsOrderByDate(2)
                .Returns(Task.FromResult(new Account
                {
                    Id = 2,
                    Balance = 30000,
                    Payments = new Payment[] { }
                }
                ));

            _accountBusiness = new AccountBusiness(accountRepository);
        }

        /// <summary>
        /// Test Get Account business logic.
        /// </summary>
        [Theory]
        [InlineData(1, true, 1000, 3)]
        [InlineData(2, true, 30000, 0)]
        [InlineData(3, false, 0, 0)]
        public async Task GetAccountTestAsync(int accountId, bool existingAccount, double expectedBalance, int expectedCountOfPayments)
        {
            var result = await _accountBusiness.GetAccountByIdIncludingPayments(accountId);
            if (existingAccount)
            {
                //check account value
                Assert.NotNull(result);
                Assert.Equal(expectedBalance, result.Balance);
                Assert.Equal(expectedCountOfPayments, result.Payments.Count);
            }
            else
            {
                //null check
                Assert.Null(result);
            }
        }
    }
}
