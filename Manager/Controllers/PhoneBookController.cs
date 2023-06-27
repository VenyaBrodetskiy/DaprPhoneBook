using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly ILogger<PhoneBookController> _logger;
        private readonly DaprClient _daprClient;

        public PhoneBookController(ILogger<PhoneBookController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet("/phonebook")]
        public async Task<ActionResult<List<string>>> GetAllPhonesAsync()
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<string>>(HttpMethod.Get, "phoneaccessor", "/phonebook");

                _logger.LogInformation("result returned from phoneaccessor");

                if (result is null)
                {
                    _logger.LogInformation("Request from DB return empty list of phones");
                    return NotFound("phones not found");
                }
                else
                {
                    _logger.LogInformation("List of phones successfully retrieved from DB");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}