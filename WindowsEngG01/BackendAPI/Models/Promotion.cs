using System;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class Promotion
    {
        [Key]
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public String CompanyId { get; set; }
        public Uri PDFUri { get; set; }

    }
}