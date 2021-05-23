using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class ProstorijaDTO
    {
        public String id { get; set; }
        public String idBolnice { get; set; }
        public TipProstorije tipProstorije { get; set; }
        public String broj { get; set; }
        public int sprat { get; set; }
        public bool dostupnost { get; set; }
        public bool logickiObrisana { get; set; }
        public int brojZauzetihKreveta { get; set; }
        public List<Stavka> Stavka { get; set; }
    }
}
