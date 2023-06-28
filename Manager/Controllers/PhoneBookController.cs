using Dapr.Client;
using Manager.Models;
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
        public async Task<ActionResult<List<PhoneName>>> GetAllPhonesAsync()
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<PhoneName>>(HttpMethod.Get, "accessor", "/phonebook");

                if (result is null)
                {
                    _logger.LogInformation("Request from acessor service return empty list of phones");
                    return NotFound("phones not found");
                }
                else
                {
                    _logger.LogInformation("List of phones successfully retrieved from accessor service");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("/phonebook")]
        public async Task<ActionResult<PhoneName>> AddPhoneAsync(PhoneName phoneName)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<PhoneName, PhoneName>(HttpMethod.Post, "accessor", "/phonebook", phoneName);

                if (result is null)
                {
                    _logger.LogInformation("Couldn't add phone");
                    return BadRequest("Couldn't add phone");
                }
                else
                {
                    _logger.LogInformation("Sucessfully added");
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