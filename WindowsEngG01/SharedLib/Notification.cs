using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    public class Notification
    {
        public String Id { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public String UserId { get; set; }
        public Uri Logo { get; set; }
        public bool IsRead { get; set; }
        public String PromotionId { get; set; }
        public int PromotionTypeIdentifier { get; set; }
        public String PromotionType { get; set; }
        public int NotificationType { get; set; }
        public String Message { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid().ToString();
            IsRead = false;
        }

        public enum AllowedNotificationTypes
        {
            CREATED = 0,
            UPDATED = 1,
            DELETED = 2
        }
    }
}
