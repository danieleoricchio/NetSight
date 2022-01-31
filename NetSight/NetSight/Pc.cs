using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSight
{
    public class Pc
    {
        public bool Stato { get; set; }

        public Pc(bool stato)
        {
            this.Stato = stato;
        }
    }
}
