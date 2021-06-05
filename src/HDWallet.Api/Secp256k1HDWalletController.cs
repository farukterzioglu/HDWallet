using System;
using HDWallet.Core;
using HDWallet.Secp256k1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api
{
    public class Secp256k1HDWalletController<TWallet> : ControllerBase where TWallet: Wallet, new()
    {
        private readonly ILogger<Secp256k1HDWalletController<TWallet>> _logger;
        private readonly IHDWallet<TWallet> _hDWallet;

        public Secp256k1HDWalletController(
            ILogger<Secp256k1HDWalletController<TWallet>> logger,
            IServiceProvider prov)
        {
            _logger = logger;
            _hDWallet = prov.GetService<IHDWallet<TWallet>>();
        }

        protected ActionResult<string> DepositWallet(uint accountNumber, uint index)
        {
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetExternalWallet(index);
            return wallet.Address;
        }
        protected ActionResult<string> ChangeWallet(uint accountNumber, uint index)
        {
            if(_hDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with Mnemonic! Hd Wallet is not available.");
            }

            var wallet = _hDWallet.GetAccount(accountNumber).GetInternalWallet(index);
            return wallet.Address;
        }
    }
}