using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class StavkaDTO
    {
        public String id { get; set; }
        public String naziv { get; set; }
        public int kolicina { get; set; }
        public String proizvodjac { get; set; }
        public bool jeStaticka { get; set; }
        public bool jePotrosnaRoba { get; set; }
    }
}
