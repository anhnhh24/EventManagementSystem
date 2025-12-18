using Microsoft.AspNetCore.SignalR;

namespace EventController.Hubs
{
    public class NotificationHub : Hub
    {
        // Client can join a group for their user ID to receive personal notifications
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        // Client can leave the group
        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
        }
    }
}
