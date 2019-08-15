using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    public class Notification
    {
        public String Id { get; set; }
        public String CompanyId { get; set; }
        public String UserId { get; set; }
        public String PromotionId { get; set; }
        public int PromotionTypeIdentifier { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
