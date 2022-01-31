using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    public class Utente
    {
        public string nomeUtente;
        public string password;
        public Utente(string nomeUtente, string password)
        {
            this.nomeUtente = nomeUtente;
            this.password = password;
        }

        public string httpRequest()
        {
            string html = string.Empty;
            string url = @"http://172.16.102.70/dippolito/index.php?nomeUtente=" + nomeUtente + "&password=" + password;


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            using (Stream stream   = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }
}
