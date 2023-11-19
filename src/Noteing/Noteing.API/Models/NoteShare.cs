namespace Noteing.API.Models
{
    public class NoteShare
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }    
        public Guid AccountId { get; set; }
    }
}
