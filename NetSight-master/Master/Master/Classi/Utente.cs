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
        public string email, password, nome, cognome;
        public int cod;
        public bool valid { get; private set; }
        public bool admin;
        public Utente(){}
        public static Utente? GetUserObject(string email, string password)
        {
            var values = new Dictionary<string, string>{{ "email", email },{ "password", password },{ "account_type", "admin" } };
            JsonMessage message = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_confirmLogin, values);
            Utente utente = message.GetResultObject<Utente>();
            if (utente != null)
            {
                return utente;
            }
            else
            {
                return null;
            }
        }
    }
}
