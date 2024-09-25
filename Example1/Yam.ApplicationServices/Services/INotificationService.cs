using Yam.Model.Models;

namespace Yam.ApplicationServices.Services
{
    public interface INotificationService
    {
        void Send(Notification notification);
    }
}
