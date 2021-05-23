using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class BolnickoLecenjeDTO
    {
        public String id { get; set; }
        public DateTime datumPocetka { get; set; }
        public int trajanje { get; set; }

        public bool jeZavrsen { get; set; }
        public String idPacijenta { get; set; }
        public String idProstorije { get; set; }

        public String idTermina { get; set; }

        public BolnickoLecenjeDTO(string id, DateTime datumPocetka, int trajanje, bool jeZavrsen, string idPacijenta, string idProstorije, String idTermina)
        {
            this.id = id;
            this.datumPocetka = datumPocetka;
            this.trajanje = trajanje;
            this.jeZavrsen = jeZavrsen;
            this.idPacijenta = idPacijenta;
            this.idProstorije = idProstorije;
            this.idTermina = idTermina;
        }
    }
}
