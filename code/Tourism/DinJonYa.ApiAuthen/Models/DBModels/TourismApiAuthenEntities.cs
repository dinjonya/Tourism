using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.ApiAuthen.Models.DBModels
{
    public class TourismApiAuthenEntities : DbContext
    {
        public DbSet<DbModel_ApiUsers> ApiUsers { get; set; }
        public DbSet<DbModel_ApiUserIps> ApiUserIps { get; set; }
    }
}
