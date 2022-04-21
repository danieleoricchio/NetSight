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

        public static async Task<Utente> GetUserObject(string email, string password)
        {
            var values = new Dictionary<string, string>{{ "email", email },{ "password", password }};
            var response = await client.PostAsync("http://localhost/server/gestioneprogetto/server/confirmLogin.php", new FormUrlEncodedContent(values));
            var responseString = await response.Content.ReadAsStringAsync();
            JsonMessage message = JsonConvert.DeserializeObject<JsonMessage>(responseString);
            return new Utente(message.result) { email =  email, password = password};
        }
    }
}
