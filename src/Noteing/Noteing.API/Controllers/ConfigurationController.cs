using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noteing.API.Models;

namespace Noteing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_configuration.GetSection("APIS").Get(typeof(List<ApiSettings>)));
        }
    }
}
