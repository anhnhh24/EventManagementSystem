using Microsoft.AspNetCore.SignalR;

namespace EventController.Hubs
{
    public class TicketHub : Hub
    {
        // Client joins a group for a specific event to receive ticket availability updates
        public async Task JoinEventGroup(string eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"event_{eventId}");
        }

        // Client leaves the event group
        public async Task LeaveEventGroup(string eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"event_{eventId}");
        }

        // Organizer joins group to manage tickets for their event
        public async Task JoinOrganizerGroup(string eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"organizer_{eventId}");
        }

        // Organizer leaves the management group
        public async Task LeaveOrganizerGroup(string eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"organizer_{eventId}");
        }
    }
}
