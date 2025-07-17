using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using EventController.Models.Data.DBcontext;
using Microsoft.EntityFrameworkCore;
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
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DBContext>();
                var eventDAO = new EventDAO(context);

                eventDAO.UpdateEventStatuses();
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); 
        }
    }

}
