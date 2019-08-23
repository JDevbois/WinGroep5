using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.DataManagers
{
    public static class NotificationManager
    {
        private static List<Notification> notifications = new List<Notification>()
        {

        };

        public static void CreateNotification(String userid, String companyid, String promotionid, int notificationType)
        {
            notifications.Add(new Notification
            {
                UserId = userid,
                Logo = new CompanyManager().FindCompanyById(companyid).Logo,
                CompanyName = new CompanyManager().FindCompanyById(companyid).Name,
                CompanyId = companyid,
                PromotionId = promotionid,
                PromotionType = CompanyManager.ReturnPromotionType(companyid, promotionid),
                Message = GenerateMessage(notificationType),
                NotificationType = notificationType
            });
        }

        private static string GenerateMessage(int notificationType)
        {
            switch (notificationType)
            {
                case (int)Notification.AllowedNotificationTypes.CREATED:
                    return "A company you're subscribed to has started a promotion.";
                case (int)Notification.AllowedNotificationTypes.UPDATED:
                    return "A company you're subscribed to has updated the details of one of their ongoing promotions.";
                case (int)Notification.AllowedNotificationTypes.DELETED:
                    return "A company you're subscribed to has stopped one of their ongoing promotions.";
                default:
                    return null;
            }
        }

        internal static void MarkAsRead(string id)
        {
            notifications.Find(n => n.Id.Equals(id)).IsRead = true;
        }

        public static Notification GetNotification(String id)
        {
            return notifications.Find(n => n.Id.Equals(id));
        }

        public static List<Notification> GetNotificationsForUser(string userid)
        {
            return notifications.FindAll(n => n.UserId.Equals(userid));
        }

        public static void DeleteNotification(String id)
        {
            notifications.RemoveAll(n => n.Id.Equals(id));
        }
    }
}
