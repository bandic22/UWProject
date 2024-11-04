using System;
using UWProject.Models;

public interface INotificationService
{
    void SendAllNotifications();
    void AddNotification(string notification);
}

public class AdminNotificationService : INotificationService
{
    private bool NewNotifications { get; set; }
    private List<Notification> Notifications { get; set; }

    public AdminNotificationService()
    {
        Notifications = new List<Notification>();
        NewNotifications = false;
    }

    /// <summary>
    /// Adds a new notification
    /// </summary>
    /// <param name="notificationMessage"></param>
    public void AddNotification(string notificationMessage)
    {
        Notifications.Add(new Notification(Notifications.Count + 1, notificationMessage));
    }

    /// <summary>
    /// Sends all outstanding notifications to the admin
    /// </summary>
    public void SendAllNotifications()
    {
        Console.WriteLine("\nSending outstanding notifications to admin...\n");

        foreach (var notification in Notifications)
        {
            if (notification.IsSent == false)
            {
                Console.WriteLine($"<{notification.Message}>");
                notification.IsSent = true;
                NewNotifications = true;
            }
        }

        if (NewNotifications == false)
        {
            Console.WriteLine("\nNo new notifications.\n");
        }
    }
}
