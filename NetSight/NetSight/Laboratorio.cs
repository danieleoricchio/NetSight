using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    public class Laboratorio
    {
        public List<Pc> listaPc;

        public Laboratorio()
        {
            listaPc = new List<Pc>();
        }

        public void addPc(Pc p)
        {
            listaPc.Add(p);
        }
    }
}
