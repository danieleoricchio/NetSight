using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Master.Classi
{
    public static class PhpLinkManager
    {
        public static string URL_getLabsNames { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLabsNames.php"; } }
        public static string URL_getLab { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getLab.php"; } }
        public static string URL_getEdificiNames { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getEdificiNames.php"; } }
        public static string URL_confirmLogin { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/confirmLogin.php"; } }
        public static string URL_add { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/add.php"; } }
        public static string URL_delete { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/delete.php"; } }
        public static string URL_getEdificio { get { return "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/getEdificio.php"; } }

        private static HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) };
        public static T? GetMethod<T>(string url, Dictionary<string, string> getValues)
        {
            string param = "";
            if (getValues != null)
            {
                List<string> vs = new List<string>();
                foreach (var item in getValues)
                {
                    vs.Add(item.Key + "=" + item.Value);
                }
                param = "?" + String.Join("&", vs);
            }
            try
            {
                string result = client.GetStringAsync(url + param).Result;
                return JsonConvert.DeserializeObject<T>(result);
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
                string ris = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(ris);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
