using eXercise.Entities;
using eXercise.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace eXercise.Controllers
{
    public class TrolleyController : BaseController
    {
        private readonly ILogger<TrolleyController> _logger;
        private readonly ITrolleyService _trolleyService;

        public TrolleyController(ILogger<TrolleyController> logger,
                                ITrolleyService trolleyService)
        {
            _logger = logger;
            _trolleyService = trolleyService;
        }

        [HttpPost("trolleyTotal")]
        public async Task<ActionResult<decimal>> GetTrolleyTotalAsync([FromBody]TrolleyRequest trolleyRequest)
        {
            if(trolleyRequest == null)
            {
                return BadRequest("Request body is empty or null");
            }

            var result = await _trolleyService.GetTrolleyTotalAsync(trolleyRequest);

            return Ok(result);
        }
    }
}
