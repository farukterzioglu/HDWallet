using System;
using HDWallet.Core;
using HDWallet.Tron;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers.Tron
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class TronWalletController : Secp256k1WalletController<TronWallet>
    {
        public TronWalletController(
            ILogger<TronWalletController> logger,
            Func<IAccountHDWallet<TronWallet>> accountHDWallet) : base(logger, accountHDWallet) {}

        [HttpGet("/Tron/external/{index}")]
        public ActionResult<string> GetAccountDeposit(uint addressIndex)
        {
            return base.DepositWallet(addressIndex);
        }

        [HttpGet("/Tron/internal/{index}")]
        public ActionResult<string> GetAccountChange(uint addressIndex)
        {
            return base.Accepted(addressIndex);
        }
    }
}
