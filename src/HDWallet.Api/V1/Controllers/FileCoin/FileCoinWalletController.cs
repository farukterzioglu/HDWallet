using System;
using HDWallet.Core;
using HDWallet.FileCoin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.V1.Controllers.FileCoin
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}")]
    public class FileCoinWalletController : Secp256k1WalletController<FileCoinWallet>
    {
        public FileCoinWalletController(
            ILogger<FileCoinWalletController> logger,
            Func<IAccountHDWallet<FileCoinWallet>> accountHDWallet) : base(logger, accountHDWallet) {}

        [HttpGet("/FileCoin/external/{index}")]
        public ActionResult<string> GetAccountDeposit(uint index) => base.DepositWallet(index);

        [HttpGet("/FileCoin/internal/{index}")]
        public ActionResult<string> GetAccountChange(uint index) => base.Accepted(index);
    }
}
