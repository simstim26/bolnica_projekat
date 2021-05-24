using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    public class ProstorijaRenoviranje
    {
        public String idProstorije { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumKraja { get; set; }
        public String razlogRenoviranja { get; set; }
        public int tipRenoviranja { get; set; }

        public String idProstorijeKojaSeSpaja { get; set; }

        public ProstorijaRenoviranje()
        {

        }

        public ProstorijaRenoviranje(String idProstorije, DateTime datumPocetka, DateTime datumKraja, String razlogRenovirana, int tipRenoviranja)
        {
            this.idProstorije = idProstorije;
            this.datumPocetka = datumPocetka;
            this.datumKraja = datumKraja;
            this.razlogRenoviranja = razlogRenovirana;
            this.tipRenoviranja = tipRenoviranja;
        }
    }
}
