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
    public class FileCoinHDWalletController : Secp256k1HDWalletController<FileCoinWallet>
    {
        public FileCoinHDWalletController(
            ILogger<FileCoinHDWalletController> logger,
            IServiceProvider prov) : base(logger, prov) {}

        [HttpGet("/FileCoin/{account}/external/{index}")]
        public ActionResult<string> GetDeposit(uint account, uint index) => base.DepositWallet(account, index);

        [HttpGet("/FileCoin/{account}/internal/{index}")]
        public ActionResult<string> GetChange(uint account, uint index) => base.ChangeWallet(account, index);
    }
}
