using DinJonYa.Plugs.Time;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.ApiAuthen.Models.DBModels
{
    public class SampleData : DropCreateDatabaseIfModelChanges<TourismApiAuthenEntities>
    {
        protected override void Seed(TourismApiAuthenEntities context)
        {
            List<DbModel_ApiUsers> apiUsers = new List<DbModel_ApiUsers>
            {
                new DbModel_ApiUsers
                {
                    AppKey ="ee13f936f8f94fbeb739739d937f5d70",
                    AppSecrect = "fc611f2be8590f0252fd9e3f7e89e565",    //md5(pwd+salt+AppName)    pwd = md5(md5(CreateTime)) = 0cf1b2b809262d5ce98ecc40e4cdb88a
                    AppName="Telecom.TourismUI",
                    CreateTokenTime = "1479196584",  //UnixTimeHelper.FromDateTime(DateTime.Now).ToString(),
                    Salt = "TDZOWkZQNDQU=",         //TDZOWkZQNDQU=
                    Token = ""
                }
            };
            context.ApiUsers.AddRange(apiUsers);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
