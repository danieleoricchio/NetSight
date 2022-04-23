using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public class Utente
    {
        private static readonly HttpClient client = new HttpClient();
        private const string url_confirmLogin = "http://housetesting.ddns.net:9050/server/gestioneprogetto/server/confirmLogin.php";
        public string email;
        public string password;
        public bool valid { get; private set; }
        private Utente(bool result)
        {
            valid = result;
            if (!result)
            {
                this.email = null;
                this.password = null;
                return;
            }
        }
        public Utente(string email, string password)
        {
            valid = false;
            this.email = null;
            this.password = null;
        }

        public static Utente GetUserObject(string email, string password)
        {
            var values = new Dictionary<string, string>{{ "email", email },{ "password", password }};
            var response = client.PostAsync(url_confirmLogin, new FormUrlEncodedContent(values)).Result;
            JsonMessage message = JsonConvert.DeserializeObject<JsonMessage>(response.Content.ReadAsStringAsync().Result);
            return new Utente(message.result) { email =  email, password = password};
        }
    }
}
