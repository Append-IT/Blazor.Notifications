using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Append.Blazor.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        /// <inheritdoc/>
        public PermissionType PermissionStatus { get; private set; }
        public NotificationService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Append.Blazor.Notifications/scripts.js").AsTask());
        }

        /// <inheritdoc/>
        public async ValueTask<bool> IsSupportedByBrowserAsync()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }

        /// <inheritdoc/>
        public async ValueTask<PermissionType> RequestPermissionAsync()
        {
            var module = await _moduleTask.Value;
            string permission = await module.InvokeAsync<string>("requestPermission");

            if (permission.Equals("granted", StringComparison.InvariantCultureIgnoreCase))
                PermissionStatus = PermissionType.Granted;

            if (permission.Equals("denied", StringComparison.InvariantCultureIgnoreCase))
                PermissionStatus = PermissionType.Denied;

            return PermissionStatus;
        }

        /// <inheritdoc/>
        public async ValueTask<Notification> CreateAsync(string title, NotificationOptions options)
        {
            var module = await _moduleTask.Value;
            var jsObject = await module.InvokeAsync<IJSObjectReference>("create", title, options);
            return new Notification(title, jsObject, options);
        }

        /// <inheritdoc/>
        public async ValueTask<Notification> CreateAsync(string title, string body, string icon = null)
        {
            var module = await _moduleTask.Value;

            NotificationOptions options = new NotificationOptions
            {
                Body = body,
                Icon = icon,
            };

            var jsObject = await module.InvokeAsync<IJSObjectReference>("create", title, options);
            return new Notification(title, jsObject, options);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
