using Microsoft.AspNetCore.Mvc;
using NamesAPI.Application;
using NamesAPI.Domain;

namespace NamesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/names")] 
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService personService, ILogger<PersonController> logger) {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PersonFilters? filters)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Bad Request for {filters}", filters);
                return BadRequest(ModelState);
            }
            try
            {
                var personList = await _personService.GetByFilter(filters);
                _logger.LogInformation("The request was successful for {filters}", filters);
                return Ok(personList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request for {filters}", filters);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
