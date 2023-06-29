using Accessor.Models;
using Accessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookAccessorController : ControllerBase
    {
        private readonly ILogger<PhoneBookAccessorController> _logger;
        private readonly PhoneBookService _phoneBookService;

        public PhoneBookAccessorController(ILogger<PhoneBookAccessorController> logger, PhoneBookService phoneBookService)
        {
            _logger = logger;
            _phoneBookService = phoneBookService;
        }

        [HttpGet("/phonebooks")]
        public async Task<ActionResult<List<PhoneName>>> GetAllPhonesAsync()
        {
            try
            {
                _logger.LogInformation("in GetAllPhonesAsync method");

                var result = await _phoneBookService.GetAllAsync();

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

        [HttpGet("/phonebook/{phone}")]
        public async Task<ActionResult<List<PhoneName>>> GetPhoneByNameAsync(string phone)
        {
            try
            {
                _logger.LogInformation("in GetPhoneByNameAsync method");

                var result = await _phoneBookService.GetByNameAsync(phone);

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

        [HttpDelete("/phonebook/{phone}")]
        public async Task<ActionResult<long>> DeletePhoneAsync(string phone)
        {
            try
            {
                _logger.LogInformation("in DeletePhoneByNameAsync method");

                var result = await _phoneBookService.DeletePhoneAsync(phone);

                _logger.LogInformation("Deleted {result} phone with name: {name}", result, phone);
                return Ok(result);
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
                _logger.LogInformation("in AddPhoneAsync method");

                var result = await _phoneBookService.AddPhoneNameAsync(phoneName);

                if (result is null)
                {
                    _logger.LogInformation("Couldn't add phoneName to db");
                    return BadRequest("Couldn't add phoneName to db");
                }
                else
                {
                    _logger.LogInformation("Successfully added");
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