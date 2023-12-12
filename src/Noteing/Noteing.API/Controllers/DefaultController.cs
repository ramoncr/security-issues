using Microsoft.AspNetCore.Mvc;

namespace Noteing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IConfiguration _configuration;

        public DefaultController(ILogger<DefaultController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Hello World! How awesome of you that you are using our product. We would love to hear what you think of it! You can provide us feedback on awesome@noteing.zip. For now lets get you started. To initially setup the identity server go to /setup and use login admin:aas908df70tagasd");
        }

        [HttpGet("/setup")]
        public IActionResult Setup()
        {

            var authHeader = Request.Headers.Authorization;
            if (!authHeader.Contains("Basic"))
                return Unauthorized();


            var headerSegments = authHeader.ToString().Split(' ');
            if (headerSegments.Count() < 2)
                return Unauthorized();

            var authBaseBlock = headerSegments[1];
            var authBlock = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(authBaseBlock));
            var authBlockSegments = authBlock.Split(':');

            if (authBlockSegments.Count() < 2)
                return Unauthorized();

            var username = authBlockSegments[0];
            var password = authBlockSegments[1];

            if (username.Equals(_configuration.GetValue<string>("Default:Setup:Credentials:Username"), StringComparison.OrdinalIgnoreCase) && password.Equals(_configuration.GetValue<string>("Default:Setup:Credentials:Password"), StringComparison.OrdinalIgnoreCase))
            {
                return new RedirectResult("/internal/setup/step-01");
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
