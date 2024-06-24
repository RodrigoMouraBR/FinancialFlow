using FinancialFlow.Core.Notifications;

namespace FinancialFlow.Core.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
