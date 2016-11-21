using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.Aop.Models.DBModels
{
    public class DinJonYaAopEntities : DbContext
    {
        public DbSet<DbModel_SensitiveWords> SensitiveWords { get; set; }
    }
}
