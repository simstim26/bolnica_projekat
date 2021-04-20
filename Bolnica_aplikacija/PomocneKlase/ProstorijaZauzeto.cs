using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class ProstorijaZauzeto
    {
        public String idProstorije { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumKraja { get; set; }

        public bool jeZavrseno { get; set; }
    }
}
