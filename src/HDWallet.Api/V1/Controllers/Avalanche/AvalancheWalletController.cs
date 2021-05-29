using System;
using HDWallet.Avalanche;
using HDWallet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers.Avalanche
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class AvalancheWalletController : Secp256k1WalletController<AvalancheWallet>
    {
        public AvalancheWalletController(
            ILogger<AvalancheWalletController> logger,
            Func<IAccountHDWallet<AvalancheWallet>> accountHDWallet) : base(logger, accountHDWallet) {}

        [HttpGet("/Avalanche/external/{index}")]
        public ActionResult<string> GetAccountDeposit(uint addressIndex) => base.DepositWallet(addressIndex);

        [HttpGet("/Avalanche/internal/{index}")]
        public ActionResult<string> GetAccountChange(uint addressIndex) => base.Accepted(addressIndex);
    }
}
