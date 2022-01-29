using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
