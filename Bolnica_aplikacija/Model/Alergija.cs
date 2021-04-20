using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class Alergija
    {
        public String idPacijenta { get; set; }
        public String nazivAlergije { get; set; }

        public Alergija(String idPacijenta, String nazivAlergije)
        {
            this.idPacijenta = idPacijenta;
            this.nazivAlergije = nazivAlergije;
           
        }

    }
}
