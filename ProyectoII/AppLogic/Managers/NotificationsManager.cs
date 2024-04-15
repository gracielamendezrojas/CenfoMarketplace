using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class NotificationsManager : BaseManager
    {
        private NotificationCrudFactory notCrudFactory;

        public NotificationsManager()
        {
            notCrudFactory = new NotificationCrudFactory();
        }

        public void CreateNotifications(Notification Notifications)
        {
            notCrudFactory.Create(Notifications);
        }

        public List<Notification> GetNotifications()
        {
            return notCrudFactory.RetrieveAll<Notification>();
        }

        public void DeleteNotifications(Notification Notifications)
        {
            notCrudFactory.Delete(Notifications);
        }

        public Notification GetNotifications(Notification Notifications)
        {
            return notCrudFactory.Retrieve<Notification>(Notifications);
        }
        public List<Notification> GetNotificationsUserId(Notification Notifications)
        {
            return notCrudFactory.RetrieveAllUserId<Notification>(Notifications);
        }
        public void EditNotifications(Notification Notifications)
        {
            notCrudFactory.Update(Notifications);
        }

    }
}
