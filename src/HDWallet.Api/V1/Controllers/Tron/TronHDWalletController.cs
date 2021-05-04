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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TronHDWalletController : ControllerBase
    {
        private readonly ILogger<TronHDWalletController> _logger;
        private readonly IHDWallet<TronWallet> _hDWallet;

        public TronHDWalletController(
            ILogger<TronHDWalletController> logger,
            Func<IHDWallet<TronWallet>> hDWallet)
        {
            _logger = logger;
            _hDWallet= hDWallet();
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
    }
}
