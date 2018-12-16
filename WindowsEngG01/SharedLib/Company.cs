using System;
using System.Collections.ObjectModel;

namespace SharedLib
{
    public class Company
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String PostalCode { get; set; }
        public String Number { get; set; }
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<OpeningHour> OpeningsHours { get; set; }
        public ObservableCollection<Subscription> Subscriptions { get; set; }
        public String Type { get; set; }
        public String Website { get; set; }
        public Uri Logo { get; set; }
    }
}
