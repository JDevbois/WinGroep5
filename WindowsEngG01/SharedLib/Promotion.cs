﻿using System;

namespace SharedLib
{
    public class Promotion
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompanyID { get; set; }
    }
}
