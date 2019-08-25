using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackendAPI.Models
{
    public class Company
    {
        [Key]
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
    }
}