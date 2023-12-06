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
        private readonly SummarizeService sentimentService;
        private readonly ApplicationDbContext dbContext;

        public PremiumController(SummarizeService sentimentService, ApplicationDbContext dbContext)
        {
            this.sentimentService = sentimentService;
            this.dbContext = dbContext;
        }

        [HttpGet("summary/{nodeId:guid}")]
        public IActionResult Summarize(Guid noteId)
        {
            var result = dbContext.Notes.First(x => x.Id == noteId);
            if (result == null)
                return NotFound();

            return Ok(sentimentService.SummarizeMessage(result.Content));
        }
    }
}
