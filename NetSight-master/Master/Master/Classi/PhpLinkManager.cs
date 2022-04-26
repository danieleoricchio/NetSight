using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Master.Classi
{
    public static class PhpLinkManager
    {
        public static string URL_getLabsNames { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLabsNames.php?codedificio="; } }
        public static string URL_getLab { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLab.php?name="; } }
        public static string URL_getEdifici { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getEdifici.php"; } } 
        public static string URL_confirmLogin { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/confirmLogin.php"; } } 
        public static string URL_add { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/add.php"; } } 

        private static HttpClient client = new HttpClient() { Timeout= TimeSpan.FromSeconds(5) };
        public static T? GetMethod<T>(string url)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(client.GetStringAsync(url).Result);
            }
            catch (Exception)
            {
                return default(T);
            }
            
        }
        public static T? PostMethod<T>(string url, Dictionary<string, string> postValues)
        {
            try
            {
                var response = client.PostAsync(url, new FormUrlEncodedContent(postValues)).Result;
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
