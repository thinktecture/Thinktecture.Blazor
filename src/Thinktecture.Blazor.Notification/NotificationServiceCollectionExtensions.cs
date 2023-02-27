using Microsoft.Extensions.DependencyInjection;
using Thinktecture.Blazor.Notification.Services;

namespace Thinktecture.Blazor.Notification {
    public static class NotificationServiceCollectionExtensions {
        public static IServiceCollection AddNotificationService(this IServiceCollection services) {
            return services.AddScoped<NotificationService>();
        }
    }
}
