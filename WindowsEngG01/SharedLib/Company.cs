using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharedLib
{
    public class Company
    {
        public String Id { get; set; }
        public String UserId { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String PostalCode { get; set; }
        public String Number { get; set; }
        public bool IsSpotlighted { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Event> Events { get; set; }
        public List<DiscountCoupon> DiscountCoupons { get; set; }
        public string OpeningHours { get; set; }
        public String Type { get; set; }
        public String Website { get; set; }
        public Uri Logo { get; set; }
        //TODO should be static
        public List<String> AllowedTypes { get; set; }

        public Company()
        {
            Id = Guid.NewGuid().ToString();
            IsSpotlighted = false;
            Promotions = new List<Promotion>();
            Events = new List<Event>();
            DiscountCoupons = new List<DiscountCoupon>();
            AllowedTypes = new List<String>
            {
                "Night Shop",
                "Supermarket",
                "Food Shop",
                "Clothes Shop",
                "Restaurant",
                "School",
                "Hotel",
                "Other"
            };
        }
    }
}
