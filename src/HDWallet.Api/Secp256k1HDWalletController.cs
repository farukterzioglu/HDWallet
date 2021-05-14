using System;
using HDWallet.Core;
using HDWallet.Secp256k1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api
{
    public class Secp256k1HDWalletController<TWallet> : ControllerBase where TWallet: Wallet, new()
    {
        private readonly ILogger<Secp256k1HDWalletController<TWallet>> _logger;
        private readonly IHDWallet<TWallet> _hDWallet;

        public Secp256k1HDWalletController(
            ILogger<Secp256k1HDWalletController<TWallet>> logger,
            Func<IHDWallet<TWallet>> hDWallet)
        {
            _logger = logger;
            _hDWallet= hDWallet();
        }

        protected ActionResult<string> DepositWallet(uint accountNumber, uint addressIndex)
        {
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetExternalWallet(addressIndex);
            return wallet.Address;
        }
        protected ActionResult<string> ChangeWallet(uint accountNumber, uint addressIndex)
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