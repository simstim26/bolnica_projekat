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
        public String idProstorijeUKojuSePrebacuje { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumKraja { get; set; }

        public bool jeZavrseno { get; set; }
        public String idStavke { get; set; }
        public int kolicinaStavke { get; set; }

        public ProstorijaZauzeto(String idProstorije, String idProstorijeUKojuSePrebacuje, DateTime datumPocetka, DateTime datumKraja,
                                 bool jeZavrseno, String idStavke, int kolicinaStavke)
        {
            this.idProstorije = idProstorije;
            this.idProstorijeUKojuSePrebacuje = idProstorijeUKojuSePrebacuje;
            this.datumPocetka = datumPocetka;
            this.datumKraja = datumKraja;
            this.jeZavrseno = jeZavrseno;
            this.idStavke = idStavke;
            this.kolicinaStavke = kolicinaStavke;
        }
    }
}
