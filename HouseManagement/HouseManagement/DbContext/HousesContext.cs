using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HouseManagement.Models;

namespace HouseManagement.DbContext
{
    public class HousesContext: System.Data.Entity.DbContext
    {
        public HousesContext(): base("Houses")
        { }

        public DbSet<House> Houses { get; set; } 
    }
}