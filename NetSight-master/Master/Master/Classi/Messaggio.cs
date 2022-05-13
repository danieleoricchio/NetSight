using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Classi
{
    public class Messaggio
    {
        public string mittente;
        public string destinatario;
        public string contenuto;
        public Messaggio(string mittente, string destinatario, string contenuto)
        {
            this.contenuto = contenuto;
            this.mittente = mittente;
            this.destinatario = destinatario;
        }
    }
}