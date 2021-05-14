using System;
using HDWallet.Core;
using HDWallet.Secp256k1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api
{
    public class Secp256k1WalletController<TWallet> : ControllerBase where TWallet: Wallet, new()
    {
        private readonly ILogger<Secp256k1WalletController<TWallet>> _logger;
        private readonly IAccountHDWallet<TWallet> _accountHDWallet;

        public Secp256k1WalletController(
            ILogger<Secp256k1WalletController<TWallet>> logger,
            Func<IAccountHDWallet<TWallet>> accountHDWallet)
        {
            _logger = logger;
            _accountHDWallet = accountHDWallet();
        }

        protected ActionResult<string> DepositWallet(uint addressIndex)
        {
            if(_accountHDWallet == null) 
            {
                return BadRequest("Wallet wasn't initialized with master key! Use hd wallet.");
            }

            var wallet = _accountHDWallet.Account.GetExternalWallet(addressIndex);
            return wallet.Address;
        }

        protected ActionResult<string> ChangeWallet(uint addressIndex)
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