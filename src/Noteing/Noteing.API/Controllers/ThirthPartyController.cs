using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noteing.API.Models;

namespace Noteing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirthPartyController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ThirthPartyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("/maps")]
        public async Task<IActionResult> GoogleMaps([FromBody] GoogleMapsRequestModel googleMapsRequestModel)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api");
            var path = googleMapsRequestModel.Path;

            path += path.Contains('?') ? '&' : '?';
            path += "key=" + configuration.GetValue<string>("Intergrations:GoogleMaps:Key");


            if (string.IsNullOrEmpty(googleMapsRequestModel.Body))
            {
                var response = await httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                return Ok(response.Content.ReadAsStringAsync());
            }
            else
            {
                var response = await httpClient.PostAsync(path, new StringContent(googleMapsRequestModel.Body));
                response.EnsureSuccessStatusCode();
                return Ok(response.Content.ReadAsStringAsync());
            }
        }
    }
}
