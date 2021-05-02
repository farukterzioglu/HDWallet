using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion( "1.0" ), ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TronController : ControllerBase
    {
        private readonly ILogger<TronController> _logger;

        public TronController(ILogger<TronController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "";
        }
    }
}
