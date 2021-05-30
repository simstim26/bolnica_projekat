using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class TerapijaPacijent
    {
        public string idTerapije { get; set; }
        public string nazivOboljenja { get; set; }
        public string nazivLeka { get; set; }
        public string opisTerapije { get; set; }

        public TerapijaPacijent(String nazivOboljenja, string nazivLeka, string opisTerapije, string idTerapije)
        {
            this.nazivLeka = nazivLeka;
            this.nazivOboljenja = nazivOboljenja;
            this.opisTerapije = opisTerapije;
            this.idTerapije = idTerapije;
        }

    }
}
