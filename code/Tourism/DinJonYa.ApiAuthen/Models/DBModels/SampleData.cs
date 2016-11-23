using DinJonYa.Plugs.Time;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.ApiAuthen.Models.DBModels
{
    public class SampleData : DropCreateDatabaseIfModelChanges<TourismApiAuthenEntities>
    {
        protected override void Seed(TourismApiAuthenEntities context)
        {
            List<DbModel_ApiUsers> apiUsers = new List<DbModel_ApiUsers>
            {
                new DbModel_ApiUsers
                {
                    AppKey ="c999d3f98425e12cc0563f3f07c731bc",     //  md5(TourismUI)
                    AppSecrect = "fc611f2be8590f0252fd9e3f7e89e565",    //md5(pwd+salt+AppName)    pwd = md5(md5(CreateTime)) = 0cf1b2b809262d5ce98ecc40e4cdb88a
                    AppName="Telecom.TourismUI",
                    CreateTokenTime = "1479196584",  //UnixTimeHelper.FromDateTime(DateTime.Now).ToString(),
                    Salt = "TDZOWkZQNDQU=",         //TDZOWkZQNDQU=
                    Token = "",
                    InnerApp = 0   //1  内部  0 外部
                },
                new DbModel_ApiUsers
                {
                    AppKey ="fdeac3e0254b7bac63d771054eb18e0e",
                    AppSecrect = "33b96ea312be47a89d9266c117d46a07",
                    AppName="Telecom.TourismWebApi",
                    CreateTokenTime = "1479786139", 
                    Salt = "NDQ2QlZUNjFY=",
                    Token = "",
                    InnerApp = 1   
                }
            };
            context.ApiUsers.AddRange(apiUsers);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
