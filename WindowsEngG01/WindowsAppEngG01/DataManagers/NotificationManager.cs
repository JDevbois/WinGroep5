using Newtonsoft.Json;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Utils;

namespace WindowsAppEngG01.DataManagers
{
    public static class NotificationManager
    {
        private static String GETString = "http://localhost:54089/api/Notifications";

        private static List<Notification> notifications = new List<Notification>()
        {

        };

        public static List<Notification> GetNotifications()
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(GETString)).Result;
                var lst = JsonConvert.DeserializeObject<List<Notification>>(json);
                return lst;

            } else
            {
                return notifications;
            }
        }

        public static async Task CreateNotificationAsync(String userid, String companyid, String promotionid, int notificationType)
        {
            var temp = new Notification
            {
                UserId = userid,
                Logo = new CompanyManager().FindCompanyById(companyid).Logo,
                CompanyName = new CompanyManager().FindCompanyById(companyid).Name,
                CompanyId = companyid,
                PromotionId = promotionid,
                PromotionType = CompanyManager.ReturnPromotionType(companyid, promotionid),
                Message = GenerateMessage(notificationType),
                NotificationType = notificationType
            };

            if (UseBackend.Backend)
            {
                var notificationJson = JsonConvert.SerializeObject(temp);

                HttpClient client = new HttpClient();
                var res = await client.PostAsync(GETString, new StringContent(notificationJson, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);

            } else
            {
                notifications.Add(temp);
            }
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

        public static Notification GetNotification(String id)
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(String.Format("{0}/{1}", GETString, id))).Result;
                var res = JsonConvert.DeserializeObject<Notification>(json);
                return res;
            }
            return notifications.Find(n => n.Id.Equals(id));
        }

        public static List<Notification> GetNotificationsForUser(string userid)
        {
            return GetNotifications().FindAll(n => n.UserId.Equals(userid));
        }

        public static async Task DeleteNotificationAsync(String id)
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var res = await client.DeleteAsync(new Uri(String.Format("{0}/{1}", GETString, id)));

            } else
            {
                notifications.RemoveAll(n => n.Id.Equals(id));
            }
        }
    }
}
