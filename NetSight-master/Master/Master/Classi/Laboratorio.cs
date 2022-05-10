using Master.Classi;
using System;
using System.Collections.Generic;

namespace Master
{
    public class Laboratorio
    {
        public List<Pc> listaPc;
        public int cod, codEdificio;
        public string nome;
        private object lockingObject = new object();

        public Laboratorio()
        {
            listaPc = new List<Pc>();
        }

        public void addPc(Pc p)
        {
            lock (lockingObject)
            {
                listaPc.Add(p);
            }
        }
        public List<Pc> GetPcs()
        {
            return listaPc;
        }

        public Pc GetPc(string nomeOrIP)
        {
            return listaPc.Find(pc => pc.nome == nomeOrIP || pc.ip == nomeOrIP);
        }

        public int getPos(Pc pc)
        {
            return listaPc.IndexOf(pc);
        }

        public bool eliminaPc(int pos)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>() { { "type","pc" },{ "nome",listaPc[pos].nome }, { "ip",listaPc[pos].ip }, { "codlab",cod.ToString() } };
                JsonMessage message = PhpLinkManager.GetMethod<JsonMessage>(PhpLinkManager.URL_delete, dic);
                if (message == null) return false;
                if (!message.status) return false;
                listaPc.RemoveAt(pos);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
