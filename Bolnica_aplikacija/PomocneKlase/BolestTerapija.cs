using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    public class BolestTerapija
    {
        public String idBolesti { get; set; }

        public String idPacijenta { get; set; }

        public String nazivBolesti { get; set; }

        public String idTerapije { get; set; }

        public String idLeka { get; set; }

        public String kolicina { get; set; }

        public String aktivnost { get; set; }

        public String nazivTerapije { get; set; }
        public String idTermina { get; set; }
        public String izvestaj { get; set; }

        public BolestTerapija() { }

        public BolestTerapija(String idBolesti, String nazivBolesti, String idTerapije, String idLeka, String kolicina, String aktivnost,
            String nazivTerapije, String idTermina, String izvestaj, String idPacijenta)
        {
            this.idBolesti = idBolesti;
            this.nazivBolesti = nazivBolesti;
            this.idTerapije = idTerapije;
            this.idLeka = idLeka;
            this.kolicina = kolicina;
            this.aktivnost = aktivnost;
            this.nazivTerapije = nazivTerapije;
            this.idTermina = idTermina;
            this.izvestaj = izvestaj;
            this.idPacijenta = idPacijenta;
        }
    }
}
