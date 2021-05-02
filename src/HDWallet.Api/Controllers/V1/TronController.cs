using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDWallet.Core;
using HDWallet.Tron;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion( "1.0" ), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TronController : ControllerBase
    {
        private readonly ILogger<TronController> _logger;
        private readonly IHDWallet<TronWallet> _hDWallet;
        private readonly IAccountHDWallet<TronWallet> _accountHDWallet;

        public TronController(
            ILogger<TronController> logger,
            Func<IHDWallet<TronWallet>> hDWallet, 
            Func<IAccountHDWallet<TronWallet>> accountHDWallet)
        {
            _logger = logger;
            _hDWallet= hDWallet();
            _accountHDWallet = accountHDWallet();
        }

        [HttpGet("/account/{accountNumber}/deposit/{addressIndex}")]
        public ActionResult<string> GetDeposit(uint accountNumber, uint addressIndex)
        {
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetExternalWallet(addressIndex);
            return wallet.Address;
        }

        [HttpGet("/account/{accountNumber}/change/{addressIndex}")]
        public ActionResult<string> GetChange(uint accountNumber, uint addressIndex)
        {
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetInternalWallet(addressIndex);
            return wallet.Address;
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
