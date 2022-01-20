using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetSight
{
    class GestionePc
    {
        public List<Pc> pcList { get; private set; }

        public GestionePc()
        {
            pcList = new List<Pc>();
        }

        public void AddPc(Pc other)
        {
            pcList.Add(other);
        }
    }
}
