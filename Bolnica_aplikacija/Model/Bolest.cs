using Model;
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
        public Terapija terapija { get; set; }
        public Pacijent pacijent { get; set; }

        public Bolest() { }

        public Bolest(string id, string naziv, Terapija terapija, Pacijent pacijent)
        {
            this.id = id;
            this.naziv = naziv;
            this.terapija = terapija;
            this.pacijent = pacijent;
        }

        public void kopiraj(Bolest bolest)
        {
            id = bolest.id;
            naziv = bolest.naziv;
            terapija = bolest.terapija;
            pacijent = bolest.pacijent;
        }

    }
}
