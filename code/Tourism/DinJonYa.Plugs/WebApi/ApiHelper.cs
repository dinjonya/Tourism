// <summary> 
//     作者：
//     时间：2016/8/30 14:18:51  
//     部门：  
//     公司：Axon
//     版权：2016-2012  
//  CLR版本：4.0.30319.42000 
//     说明：本代码版权归Axon所有
// 唯一标识：fe492679-b928-494c-ab7c-eaedf98dcdd2 
//
// 更改作者：
// 更改说明：
// </summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.Plugs.WebApi
{
    public class ApiHelper
    {

        private readonly static Object lockObj = new Object();
        private static readonly HttpClient httpPostClient = new HttpClient();

        /// <summary>
        /// 异步 Get方法调用 webApi
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径以及参数</param>
        /// <returns>返回GET到的对象，如果响应失败抛出异常</returns>
        public static async Task<T> GetWebApiAsync<T>(string webApiUri, string webApiPath, Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(webApiUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                HttpResponseMessage httpResponseMessage = await client.GetAsync(webApiPath, HttpCompletionOption.ResponseContentRead);
                httpResponseMessage.EnsureSuccessStatusCode();
                return await httpResponseMessage.Content.ReadAsAsync<T>();
            }
        }
        


        public static async Task<T> PostAsync<T>(string webApiUri, string webApiPath, Object obj, Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            httpPostClient.BaseAddress = new Uri(webApiUri);
            httpPostClient.DefaultRequestHeaders.Clear();
            httpPostClient.DefaultRequestHeaders.Accept.Clear();
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, string> customHeader in customHeaders)
                {
                    httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                }
            }
            httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            httpPostClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsJsonAsync(webApiPath, obj);
            HttpResponseMessage result = httpResponseMessage.Result;
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsAsync<T>();
        }


        /// <summary>
        /// GET 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <returns>返回GET到的json字符串</returns>
        public static T GetWebApi<T>(string webApiUri, string webApiPath,Dictionary<string,string> customHeaders=null, string MediaType = "application/json")
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webApiUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                if (customHeaders != null)
                {
                    foreach (KeyValuePair<string, string> customHeader in customHeaders)
                    {
                        client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                    }
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                Task<HttpResponseMessage>  httpResponseMessage = client.GetAsync(webApiPath, HttpCompletionOption.ResponseContentRead);
                T result = httpResponseMessage.Result.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }
        /// <summary>
        /// POST 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <param name="obj">Post的对象参数</param>
        /// <returns>返回对应的信息</returns>
        public static string PostWebApi(string webApiUri, string webApiPath, Object obj, Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            httpPostClient.BaseAddress = new Uri(webApiUri);
            httpPostClient.DefaultRequestHeaders.Accept.Clear();
            httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, string> customHeader in customHeaders)
                {
                    httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                }
            }
            lock (lockObj)
            {
                Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsJsonAsync(webApiPath, obj);
                string result = httpResponseMessage.Result.Content.ReadAsStringAsync().Result;
                return result;
            }
        }


        /// <summary>
        /// POST 方法调用webApi
        /// </summary>
        /// <param name="webApiUri">web api的uri</param>
        /// <param name="webApiPath">web api 方法路径</param>
        /// <param name="obj">Post的对象参数</param>
        /// <returns>返回对应的信息</returns>
        public static T PostWebApi<T>(string webApiUri, string webApiPath, Object obj, Dictionary<string, string> customHeaders = null, string MediaType = "application/json")
        {
            httpPostClient.BaseAddress = new Uri(webApiUri);
            httpPostClient.DefaultRequestHeaders.Accept.Clear();
            httpPostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            if (customHeaders != null)
            {
                foreach (KeyValuePair<string, string> customHeader in customHeaders)
                {
                    httpPostClient.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);
                }
            }
            lock (lockObj)
            {
                Task<HttpResponseMessage> httpResponseMessage = httpPostClient.PostAsJsonAsync(webApiPath, obj);
                T result = httpResponseMessage.Result.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }
    }

    public enum CallType
    {
        GET,
        POST
    }
}
