using Microsoft.AspNetCore.SignalR;

namespace EventController.Hubs
{
    public class CommentHub : Hub
    {
        // Client joins a group for a specific event to receive comment updates
        public async Task JoinEventGroup(string eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"event_{eventId}");
        }

        // Client leaves the event group
        public async Task LeaveEventGroup(string eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"event_{eventId}");
        }
    }
}
