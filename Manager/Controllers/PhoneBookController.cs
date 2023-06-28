using Dapr.Client;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly ILogger<PhoneBookController> _logger;
        private readonly DaprClient _daprClient;

        public PhoneBookController(ILogger<PhoneBookController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet("/phonebooks")]
        public async Task<ActionResult<List<PhoneName>>> GetAllPhonesAsync()
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<PhoneName>>(HttpMethod.Get, "accessor", "/phonebooks");

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

        [HttpGet("/phonebook")]
        public async Task<ActionResult<List<PhoneName>>> GetPhoneByNameAsync(
            [FromQuery(Name = "name")] string name)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<PhoneName>>(HttpMethod.Get, "accessor", $"/phonebook/{name}");

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

        [HttpPost("/add_phone_to_queue")]
        public async Task<ActionResult<PhoneName>> AddPhoneToQueueAsync(PhoneName phoneName)
        {
            try
            {
                await _daprClient.InvokeBindingAsync("phonequeue", "create", phoneName);

                _logger.LogInformation("Sucessfully added");
                return Ok("Sucessfully added to phonequeue");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("/phonequeue")]
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