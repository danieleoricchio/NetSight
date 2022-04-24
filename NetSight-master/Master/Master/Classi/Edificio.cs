using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Master.Classi
{
    public class Edificio
    {
        public string nome;
        public int cod;
        public string indirizzo;
        public bool valid { get; private set; }
        private Edificio(bool result)
        {
            valid = result;
            if (!result)
            {
                this.nome = null;
                this.indirizzo = null;
                return;
            }
        }
        public Edificio(string nome, string indirizzo)
        {
            valid = false;
            this.nome = null;
            this.indirizzo = null;
        }

        public static Edificio GetEdificio(string nome, string indirizzo)
        {
            var values = new Dictionary<string, string> {{ "nome", nome }, { "indirizzo", indirizzo } };
            JsonMessage message = PhpLinkManager.PostMethod<JsonMessage>(PhpLinkManager.URL_addEdificio, values);
            if (message != null)
            {
                return new Edificio(message.result) { nome = nome, indirizzo = indirizzo };
            }
            else
            {
                return new Edificio(false) { nome = nome, indirizzo = indirizzo };
            }
        }
    }
}