using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    public class Laboratorio
    {
        private List<Pc> listaPc;

        public Laboratorio()
        {
            listaPc = new List<Pc>();
        }

        public void addPc(Pc p)
        {
            listaPc.Add(p);
        }
        public List<Pc> GetPcs()
        {
            return listaPc;
        }

        public Pc GetPc(string nomeOrIP)
        {
            return listaPc.Find(pc => pc.Nome==nomeOrIP || pc.IP == nomeOrIP);
        }
    }
}
