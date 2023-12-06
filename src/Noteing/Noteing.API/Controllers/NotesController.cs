using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Noteing.API.Data;
using Noteing.API.Helpers;
using Noteing.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Noteing.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NotesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _dbContext.Notes
                .Where(note => note.Owner == IdentityHelper.GetCurrentUserId(User))
                .ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var note = _dbContext.Notes.FirstOrDefault(note => note.Id == id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public Task Post([FromBody] Note value) => ProcessChange(value);

        [HttpPut("{id}")]
        public Task Put(Guid id, [FromBody] Note value) => ProcessChange(value);

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var note = _dbContext.Notes.First(note => note.Id == id && note.Owner == IdentityHelper.GetCurrentUserId(User));
            _dbContext.Notes.Remove(note);
        }

        private async Task ProcessChange(Note value)
        {
            if (value.Id == Guid.Empty)
                value.Id = Guid.NewGuid();

            var existingNote = _dbContext.Notes.FirstOrDefault(note => note.Id == value.Id);
            if (existingNote == null)
            {
                value.Created = value.Updated = DateTimeOffset.UtcNow;
                value.Owner = IdentityHelper.GetCurrentUserId(User);
                await _dbContext.Notes.AddAsync(value);
            }
            else
            {
                existingNote.Name = value.Name;
                existingNote.Content = value.Content;
                existingNote.Updated = DateTimeOffset.UtcNow;

                _dbContext.Notes.Update(existingNote);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
