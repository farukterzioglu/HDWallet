using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDWallet.Avalanche;
using HDWallet.Core;
using HDWallet.Tron;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.Controllers.V1.Avalanche
{
    [ApiController]
    [ApiVersion( "1.0" ), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AvalancheHDWalletController : ControllerBase
    {
        private readonly ILogger<AvalancheHDWalletController> _logger;
        private readonly IHDWallet<AvalancheWallet> _hDWallet;

        public AvalancheHDWalletController(
            ILogger<AvalancheHDWalletController> logger,
            Func<IHDWallet<AvalancheWallet>> hDWallet)
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
