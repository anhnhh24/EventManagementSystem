using EventController.Models.DAO.Implements;
using EventController.Hubs;
using Microsoft.AspNetCore.SignalR;

public class EventCheckerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<NotificationHub> _notificationHub;

    public EventCheckerService(IServiceProvider serviceProvider, IHubContext<NotificationHub> notificationHub)
    {
        _serviceProvider = serviceProvider;
        _notificationHub = notificationHub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var eventDAO = scope.ServiceProvider.GetRequiredService<EventDAO>();
                    var notificationDAO = scope.ServiceProvider.GetRequiredService<NotificationDAO>();

                    // Update event statuses and broadcast changes
                    eventDAO.UpdateEventStatuses();
                    
                    // Notify users of upcoming events and broadcast via SignalR
                    var listUpcomingEvents = eventDAO.GetEventsInNextWeek();
                    await notificationDAO.NotifyUsersOfUpcomingEvents();
                    
                    // Broadcast to all connected users that event statuses have been updated
                    await _notificationHub.Clients.All.SendAsync("ReceiveEventStatusUpdate", new 
                    { 
                        message = "Event statuses have been updated",
                        timestamp = DateTime.Now
                    });
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in EventCheckerService: {ex.Message}");
        }
    }
}
