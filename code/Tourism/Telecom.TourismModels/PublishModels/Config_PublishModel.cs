using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.TourismModels.PublishModels
{
    public partial class Config_PublishModel
    {
        public Base_Config Base { get; set; }
        public Aop_Config Aop { get; set; }
        public Redis_Config Redis { get; set; }
        public Resources_Config Resources { get; set; }
        public ApiAuthen_Config ApiAuthen { get; set; }
    }
    public partial class Base_Config
    {
        public string SiteUri { get; set; }
        public string WebApiUri { get; set; }
    }

    public partial class Aop_Config
    {
        public string AopUri { get; set; }
        public RabbitMQ_Config RabbitMQ { get; set; }
    }

    public  partial class RabbitMQ_Config
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public string AssemblyName { get; set; }
        public List<RouteingKeys_Config> RouteingKeys { get; set; }
    }
    public  partial class RouteingKeys_Config
    {
        public string Key { get; set; }
        public string Handler { get; set; } 
    }

    public  partial class Redis_Config
    {
        public List<RedisIP_Config> RedisIps { get; set; }
        public bool Enabled { get; set; }
        public int KeepAlive { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
    }
    

    public  partial class RedisIP_Config
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

    public  partial class Resources_Config
    {
        public string ResourcesUri { get; set; }
        public string CssPath { get; set; }
        public string JsPath { get; set; }
        public string ImagePath { get; set; }
    }

    public partial class ApiAuthen_Config
    {
        public string ApiAuthenUri { get; set; }
        public List<ValidateMessage_Config> ValidateMessages { get; set; }
    }

    public  partial class ValidateMessage_Config
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
