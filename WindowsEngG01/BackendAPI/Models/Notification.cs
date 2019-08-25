using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAPI.Models
{
    public class Notification
    {
        public String Id { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public String UserId { get; set; }
        public Uri Logo { get; set; }
        public String PromotionId { get; set; }
        public int PromotionTypeIdentifier { get; set; }
        public String PromotionType { get; set; }
        public int NotificationType { get; set; }
        public String Message { get; set; }
    }
}