using Microsoft.AspNetCore.SignalR;

namespace Noteing.API.Hubs
{
    public class LiveUpdateHub : Hub
    {
        public async Task SubscribeToNote(Guid noteId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, noteId.ToString());
        }
    }
}
