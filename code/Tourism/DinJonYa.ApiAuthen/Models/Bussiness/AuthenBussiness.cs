﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telecom.TourismModels.ApiModels;
using DinJonYa.ApiAuthen.Models.Services;

namespace DinJonYa.ApiAuthen.Models.Bussiness
{
    public partial class AuthenBussiness
    {
        public ApiModel_Authen AddNewTokenByAppKey(string appKey,string appSecrect, string accessIp)
        {
            ApiModel_Authen model = new AuthenServices().CreateNewTokenByAppKey(appKey, appSecrect,accessIp);
            return model;
        }
    }
}