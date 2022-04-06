using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    class Laboratori
    {
        private List<string> listaLab;

        public Laboratori()
        {
            listaLab = new List<string>();
        }

        public void addPc(string lab)
        {
            listaLab.Add(lab);
        }
        public List<string> GetListaLab()
        {
            return listaLab;
        }
    }
}
