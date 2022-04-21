using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    class Laboratori
    {
        public List<Laboratorio> listaLab;

        public Laboratori()
        {
            listaLab = new List<Laboratorio>();
        }

        public void addPc(Laboratorio lab)
        {
            listaLab.Add(lab);
        }
        public List<Laboratorio> GetListaLab()
        {
            return listaLab;
        }
    }
}
