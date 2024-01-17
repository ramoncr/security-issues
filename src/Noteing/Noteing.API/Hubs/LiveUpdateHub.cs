using Microsoft.AspNetCore.SignalR;

namespace Noteing.API.Hubs
{
    public class LiveUpdateHub : Hub
    {
        public async Task SubscribeToNote(Guid? noteId)
        {
            if (noteId == null)
                noteId = Guid.Empty;

            await Groups.AddToGroupAsync(Context.ConnectionId, noteId.ToString());
        }
    }
}
