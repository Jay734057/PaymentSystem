using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core.Interfaces.Business;
using System;
using System.Threading.Tasks;

namespace PaymentSystem.Service.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountBusiness _accountBusiness;

        public AccountController(ILogger<AccountController> logger, IAccountBusiness accountBusiness)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountBusiness = accountBusiness ?? throw new ArgumentNullException(nameof(accountBusiness));
        }

        /// <summary>
        /// Get Account with a list of payments.
        /// </summary>
        /// <returns>Account with a list of payments</returns>
        /// <param name="accountId">Account id</param>
        [HttpGet]
        public async Task<IActionResult> Get(int accountId)
        {
            try
            {
                _logger.LogInformation($"Retriving account by id: {accountId}...");
                var result = await _accountBusiness.GetAccountByIdIncludingPayments(accountId);
                if(result == null)
                {
                    return NotFound($"Account with id: {accountId} doesn't exist in DB.");
                }
                _logger.LogInformation($"Retrived account by id: {accountId}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex);
                return result;
            }

        }
    }
}
