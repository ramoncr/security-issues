using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noteing.API.Data;
using Noteing.API.Services;

namespace Noteing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Premium")]
    public class PremiumController : ControllerBase
    {
        private readonly SummarizeService _summaryService;
        private readonly ApplicationDbContext _dbContext;

        public PremiumController(SummarizeService sentimentService, ApplicationDbContext dbContext)
        {
            _summaryService = sentimentService;
            _dbContext = dbContext;
        }

        [HttpGet("summary/{nodeId:guid}")]
        public IActionResult Summarize(Guid noteId, [FromQuery] string type)
        {
            var result = _dbContext.Notes.First(x => x.Id == noteId);
            if (result == null)
                return NotFound();

            return Ok(_summaryService.SummarizeMessage(result.Content, type));
        }
    }
}
