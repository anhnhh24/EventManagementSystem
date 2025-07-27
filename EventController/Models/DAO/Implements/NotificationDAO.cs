using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventController.Models.DAO.Implements
{
    public class NotificationDAO
    {
        private readonly DBContext _context;

        public NotificationDAO(DBContext context)
        {
            _context = context;
        }

        public void CreateNotification(int userId, int eventId, string message)
        {
            bool exists = _context.Notifications.Any(n =>
                n.UserID == userId &&
                n.EventID == eventId &&
                n.Message == message);

            if (!exists)
            {
                var notification = new Notification
                {
                    UserID = userId,
                    EventID = eventId,
                    Message = message,
                    SendAt = DateTime.UtcNow,
                    IsSent = false
                };

                _context.Notifications.Add(notification);
                _context.SaveChanges();
            }
        }


        public List<Notification> GetUnsentNotifications()
        {
            return _context.Notifications
                           .Include(n => n.User)
                           .Include(n => n.Event)
                           .Where(n => !n.IsSent)
                           .ToList();
        }

        public List<Notification> GetUserNotification(int userId)
        {
            return _context.Notifications
                           .Include(n => n.User)
                           .Include(n => n.Event)
                           .Where(n => n.UserID == userId).
                            OrderByDescending(n => n.SendAt)
                           .ToList();
        }

        public void MarkAsSent(Notification notification)
        {
            notification.IsSent = true;
            _context.SaveChanges();
        }

        public async Task<int> NotifyUsersOfUpcomingEvents()
        {
            EmailService emailService = new EmailService();
            int notificationsSent = 0;

            var upcomingEvents = _context.Events
                .Include(e => e.Registrations)
                    .ThenInclude(r => r.User)
                .Where(e => e.StartTime >= DateTime.UtcNow &&
                            e.StartTime <= DateTime.UtcNow.AddDays(7) &&
                            e.Status == "Upcoming")
                .ToList();

            foreach (var evt in upcomingEvents)
            {
                var users = evt.Registrations
                               .Where(r => r.Status == "Success")
                               .Select(r => r.User)
                               .Distinct()
                               .ToList();

                foreach (var user in users)
                {

                    bool alreadyNotified = _context.Notifications
                        .Any(n => n.UserID == user.UserID && n.EventID == evt.EventID);

                    if (!alreadyNotified)
                    {
                        string message = $"Reminder: The event '{evt.Title}' will take place on {evt.StartTime:dd/MM/yyyy HH:mm}. Please be ready.";
                        CreateNotification(user.UserID, evt.EventID, message);

                        string subject = $"Reminder for event: {evt.Title}";
                        string content = $"Dear {user.FullName},<br/><br/>" +
                                         $"This is a reminder for the event '{evt.Title}' scheduled on {evt.StartTime:dd/MM/yyyy HH:mm}.<br/><br/>" +
                                         "Please make sure to attend.<br/><br/>" +
                                         "Best regards,<br/>Event Team";
                        await emailService.SendConfirmationEmailAsync(user.Email,user.FullName, subject, content);

                        
                        notificationsSent++;
                    }
                }
            }

            await _context.SaveChangesAsync(); 

            return notificationsSent;
        }

    }
}
