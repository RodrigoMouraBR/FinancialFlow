using Carrefas.Core.Notifications;

namespace Carrefas.Core.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
