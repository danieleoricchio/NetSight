using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    public class Utente
    {
        private readonly HttpClient client = new HttpClient();
        public string email;
        public string password;
        public bool valid { get; private set; }
        public Utente(string email, string password)
        {
            GetUserObject(email, password);
            if (!valid)
            {
                this.email = null;
                this.password = null;
                return;
            }
            this.email = email;
            this.password = password;
        }

        private async void GetUserObject(string email, string password)
        {
            var values = new Dictionary<string, string>{{ "email", email },{ "password", password }};
            var response = await client.PostAsync("http://localhost/server/gestioneprogetto/ConfirmLogin.php", new FormUrlEncodedContent(values));
            var responseString = await response.Content.ReadAsStringAsync();
            JsonMessage message = JsonConvert.DeserializeObject<JsonMessage>(responseString);
            this.valid = message.result;
        }
    }
}
