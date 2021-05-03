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

        public ProstorijaRenoviranje(String id, DateTime pocetak, DateTime kraj, String razlogRenovirana)
        {
            this.idProstorije = id;
            this.datumPocetka = pocetak;
            this.datumKraja = kraj;
            this.razlogRenoviranja = razlogRenovirana;
        }
    }
}
