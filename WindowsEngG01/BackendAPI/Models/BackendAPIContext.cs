﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackendAPI.Models
{
    public class BackendAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BackendAPIContext() : base("BackendAPIContext")
        {
        }

        public System.Data.Entity.DbSet<BackendAPI.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<BackendAPI.Models.Promotion> Promotions { get; set; }

        public System.Data.Entity.DbSet<BackendAPI.Models.Notification> Notifications { get; set; }

        public System.Data.Entity.DbSet<BackendAPI.Models.User> Users { get; set; }
    }
}
