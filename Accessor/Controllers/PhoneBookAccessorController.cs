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

        [HttpGet("/phonebook")]
        public async Task<ActionResult<List<PhoneName>>> GetAllPhonesAsync()
        {
            try
            {
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
    }
}