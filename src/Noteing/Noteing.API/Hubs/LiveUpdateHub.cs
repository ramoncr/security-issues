using Microsoft.AspNetCore.SignalR;

namespace Noteing.API.Hubs
{
    public class LiveUpdateHub : Hub
    {
        public async Task SubscribeToNote(string noteId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Guid.Parse(noteId).ToString());
        }
    }
}
