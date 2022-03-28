using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Append.Blazor.Notifications
{
    public interface INotificationService
    {
        /// <summary>
        /// Retrieves the given or denied permission status, default if the permission was not yet requested.
        /// If you want to request permission, call <seealso cref="RequestPermissionAsync"/>
        /// </summary>
        /// <returns><seealso cref="PermissionType"/></returns>
        PermissionType PermissionStatus { get; }
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
        /// Create a Notification with <seealso cref="NotificationOptions"/>.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        /// <returns>The created <see cref="Notification"/>.</returns>
        ValueTask<Notification> CreateAsync(string title, NotificationOptions options);
        /// <summary>
        /// Creates a Notifcation with the supplied parameters.
        /// </summary>
        /// <param name="title">The title of the notification.</param>
        /// <param name="description">The body or description of the notification.</param>
        /// <param name="iconUrl">Link to a image, can be remote or served from the filesystem.</param>
        /// <returns>The created <see cref="Notification"/>.</returns>
        ValueTask<Notification> CreateAsync(string title, string description, string iconUrl = null);
    }
}
