using Master.Classi;
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
        public string email;
        public string password;
        public int cod;
        public bool valid { get; private set; }
        public bool admin;
        private Utente(bool result)
        {
            valid = result;
            if (!result)
            {
                this.email = null;
                this.password = null;
                admin = false;
                return;
            }
        }
        public Utente(string email, string password)
        {
            valid = false;
            this.email = null;
            this.password = null;
            this.admin = false;
        }

        public static Utente GetUserObject(string email, string password)
        {
            var values = new Dictionary<string, string>{{ "email", email },{ "password", password },{ "account_type", "admin" } };
            JsonMessage message = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_confirmLogin, values);
            if (message != null)
            {
                return new Utente(message.result) { email = email, password = password, admin = message.number_code==202 };
            }
            else
            {
                return new Utente(false) { email = email, password = password, admin = false };
            }
        }
    }
}
