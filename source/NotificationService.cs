using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Append.Blazor.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public NotificationService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Append.Blazor.Notifications/scripts.js").AsTask());
        }

        public async ValueTask<bool> IsSupportedByBrowserAsync()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }

        public async ValueTask<PermissionType> RequestPermissionAsync()
        {
            var module = await moduleTask.Value;
            string permission = await module.InvokeAsync<string>("requestPermission");

            if (permission.Equals("granted", StringComparison.InvariantCultureIgnoreCase))
                return PermissionType.Granted;

            if (permission.Equals("denied", StringComparison.InvariantCultureIgnoreCase))
                return PermissionType.Denied;

            return PermissionType.Default;
        }


        public async ValueTask CreateAsync(string title, NotificationOptions options) 
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("create", title, options);
        }
        

        public async ValueTask CreateAsync(string title, string body, string icon = null)
        {
            var module = await moduleTask.Value;

            NotificationOptions options = new NotificationOptions
            {
                Body = body,
                Icon = icon,
            };

            await module.InvokeVoidAsync("create", title, options);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
