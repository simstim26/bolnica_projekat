using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentModel
{
    public class PacijentTermin
    {
        public String id { get; set; }
        public String datum { get; set; }
        public String satnica { get; set; }
        public String lokacija { get; set; }
        public String napomena { get; set; }

        public String idLekara { get; set; }
        public String imeLekara { get; set; }

        public String nazivSpecijalizacije { get; set; }

        public String idSpecijalizacije { get; set; }

        public String nazivTerapije { get; set; }

        public PacijentTermin() { }

        public PacijentTermin(String id, String datum, String satnica, String lokacija, String napomena,String idLekara, String imeLekara,
            String nazivSpecijalizacije, String idSpecijalizacije, String nazivTerapije)
        {
            this.id = id;
            this.datum = datum;
            this.satnica = satnica;
            this.lokacija = lokacija;
            this.napomena = napomena;
            this.idLekara = idLekara;
            this.imeLekara = imeLekara;
            this.nazivSpecijalizacije = nazivSpecijalizacije;
            this.idSpecijalizacije = idSpecijalizacije;
            this.nazivTerapije = nazivTerapije;
        }

    }
}
