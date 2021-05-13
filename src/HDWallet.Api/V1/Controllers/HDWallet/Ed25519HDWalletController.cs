using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDWallet.Core;
using HDWallet.Ed25519;
using HDWallet.Polkadot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers.Polkadot
{
    [ApiController]
    [ApiVersion( "1.0" ), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    public class Ed25519HDWalletController : ControllerBase
    {
        private readonly ILogger<Ed25519HDWalletController> _logger;
        private readonly Func<string, IHDWallet<HDWallet.Core.IWallet>> _hDWalletSelector;

        public Ed25519HDWalletController(
            ILogger<Ed25519HDWalletController> logger,
            Func<string, IHDWallet<HDWallet.Core.IWallet>> hDWalletSelector)
        {
            _logger = logger;
            _hDWalletSelector = hDWalletSelector;
        }

        [HttpGet("/Ed25519/{coin}/account/{accountNumber}/deposit/{addressIndex}")]
        public ActionResult<string> GetDeposit(string coin, uint accountNumber, uint addressIndex)
        {
            IHDWallet<Wallet> _hDWallet = (IHDWallet<HDWallet.Ed25519.Wallet>) _hDWalletSelector(coin);
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetExternalWallet(addressIndex);
            return wallet.Address;
        }

        [HttpGet("/Ed25519/{coin}/account/{accountNumber}/change/{addressIndex}")]
        public ActionResult<string> GetChange(string coin, uint accountNumber, uint addressIndex)
        {
            IHDWallet<Wallet> _hDWallet = (IHDWallet<HDWallet.Ed25519.Wallet>) _hDWalletSelector(coin);
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetInternalWallet(addressIndex);
            return wallet.Address;
        }
    }
}
