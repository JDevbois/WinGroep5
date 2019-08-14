using System;

namespace SharedLib
{
    public class Promotion
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public String CompanyId { get; set; }
        public Uri PDFUri { get; set; }

        public String EndDateString
        {
            get { return EndDate.ToString("D"); }
        }
        public Promotion()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
