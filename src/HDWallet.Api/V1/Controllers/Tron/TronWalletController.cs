using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDWallet.Core;
using HDWallet.Tron;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers.Tron
{
    [ApiController]
    [ApiVersion( "1.0" ), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Tron")]
    public class TronWalletController : ControllerBase
    {
        private readonly ILogger<TronWalletController> _logger;
        private readonly IAccountHDWallet<TronWallet> _accountHDWallet;

        public TronWalletController(
            ILogger<TronWalletController> logger,
            Func<IAccountHDWallet<TronWallet>> accountHDWallet)
        {
            _logger = logger;
            _accountHDWallet = accountHDWallet();
        }

        [HttpGet("/deposit/{addressIndex}")]
        public ActionResult<string> GetAccountDeposit(uint addressIndex)
        {
            if(_accountHDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            }

            var wallet = _accountHDWallet.Account.GetExternalWallet(addressIndex);
            return wallet.Address;
        }

        [HttpGet("/change/{addressIndex}")]
        public ActionResult<string> GetAccountChange(uint addressIndex)
        {
            if(_accountHDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            }

            var wallet = _accountHDWallet.Account.GetInternalWallet(addressIndex);
            return wallet.Address;
        }
    }
}
