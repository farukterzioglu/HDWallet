using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HDWallet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Route("api/[controller]")]
    // [Produces(MediaTypeNames.Application.Json)]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> PostTodoItem(string value)
        {
            throw new NotImplementedException();
        }
    }
}
