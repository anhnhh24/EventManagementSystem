using EventController.Models.DAO.Implements;

public class EventCheckerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EventCheckerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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

                    eventDAO.UpdateEventStatuses();
                    var listUpcomingEvents = eventDAO.GetEventsInNextWeek();
                    await notificationDAO.NotifyUsersOfUpcomingEvents();
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
