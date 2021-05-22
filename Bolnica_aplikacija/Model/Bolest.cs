using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class Bolest
    {
        public String id { get; set; }
        public String naziv { get; set; }
        public String idTerapije { get; set; }
        public String idPacijenta { get; set; }

        public Bolest() { }

        public Bolest(string id, string naziv, string idTerapije, string idPacijenta)
        {
            this.id = id;
            this.naziv = naziv;
            this.idTerapije = idTerapije;
            this.idPacijenta = idPacijenta;
        }

        public void kopiraj(Bolest bolest)
        {
            id = bolest.id;
            naziv = bolest.naziv;
            idTerapije = bolest.idTerapije;
            idPacijenta = bolest.idPacijenta;
        }

    }
}
