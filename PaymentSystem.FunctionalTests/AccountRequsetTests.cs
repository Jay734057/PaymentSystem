using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PaymentSystem.Core.Models;
using PaymentSystem.Service;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PaymentSystem.FunctionalTests
{
    public class AccountRequsetTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AccountRequsetTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        /// <summary>
        /// Test Get Account Api.
        /// </summary>
        [Theory]
        [InlineData(1, true, 1000, 3)]
        [InlineData(2, true, 30000, 0)]
        [InlineData(3, false, 0, 0)]
        public async Task GetRequestTestAsync(int accountId, bool existingAccount, double expectedBalance, int expectedCountOfPayments)
        {
            var response = await _client.GetAsync($"/Account?accountId={accountId}");
            if (existingAccount)
            {
                //check status code
                response.EnsureSuccessStatusCode();

                //check account info in response content
                var responseString = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<Account>(responseString);
                Assert.NotNull(account);
                Assert.Equal(expectedBalance, account.Balance);
                Assert.Equal(expectedCountOfPayments, account.Payments.Count);
            }
            else
            {
                //check status code as 404 not found
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
