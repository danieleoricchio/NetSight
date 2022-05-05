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
    }
}
