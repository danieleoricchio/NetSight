using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    class Pc
    {
        public string Nome { get; private set; }
        public string ipAddress { get; private set; }
        public bool Stato { get; private set; }
        public string Luogo { get; private set; }

        public Pc(string nome, string ipAddress, bool stato, string luogo)
        {
            Nome = nome;
            this.ipAddress = ipAddress;
            Stato = stato;
            Luogo = luogo;
        }

        public Pc()
        {
            Nome = "";
            ipAddress = "";
            Stato = false;
            Luogo = "";
        }
    }
}
