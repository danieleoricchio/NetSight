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
            var response = client.PostAsync("http://housetesting.ddns.net:9050/server/gestioneprogetto/server/confirmLogin.php", new FormUrlEncodedContent(values)).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            JsonMessage message = JsonConvert.DeserializeObject<JsonMessage>(responseString);
            return new Utente(message.result) { email =  email, password = password};
        }
    }
}
