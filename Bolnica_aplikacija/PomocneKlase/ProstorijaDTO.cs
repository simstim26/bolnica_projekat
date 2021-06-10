using Bolnica_aplikacija.Model;
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
        //public ITipProstorije tipProstorije { get; set; }
        public ITipProstorije tipProstorije { get; set; }
        public String broj { get; set; }
        public int sprat { get; set; }
        public bool dostupnost { get; set; }
        public bool logickiObrisana { get; set; }
        public int brojZauzetihKreveta { get; set; }
        public List<Stavka> Stavka { get; set; }

        public ProstorijaDTO()
        {
        }

        public ProstorijaDTO(string id, string idBolnice, ITipProstorije tipProstorije, string broj, int sprat, bool dostupnost, bool logickiObrisana, int brojZauzetihKreveta, List<Stavka> stavka)
        {
            this.id = id;
            this.idBolnice = idBolnice;
            this.tipProstorije = tipProstorije;
            this.broj = broj;
            this.sprat = sprat;
            this.dostupnost = dostupnost;
            this.logickiObrisana = logickiObrisana;
            this.brojZauzetihKreveta = brojZauzetihKreveta;
            Stavka = stavka;
        }
    }
}
