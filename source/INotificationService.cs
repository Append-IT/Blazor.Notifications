using System.Threading.Tasks;

namespace Append.Blazor.Notifications
{
    public interface INotificationService
    {
        /// <summary>
        /// Checks if the Notifications' API is Support by the browser.
        /// </summary>
        /// <returns></returns>
        ValueTask<bool> IsSupportedByBrowserAsync();
        /// <summary>
        /// Request the user for his permission to send notifications.
        /// </summary>
        /// <returns></returns>
        ValueTask<PermissionType> RequestPermissionAsync();

        /// <summary>
        /// Create a Notification with <seealso cref="NotificationOptions"/>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask CreateAsync(string title, NotificationOptions options);
        ValueTask CreateAsync(string title, string description, string iconUrl = null);
    }
}
