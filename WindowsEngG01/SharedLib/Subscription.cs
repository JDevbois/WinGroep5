﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class Subscription
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
    }
}
