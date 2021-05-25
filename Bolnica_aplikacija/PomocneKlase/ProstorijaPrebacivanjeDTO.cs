using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class ProstorijaPrebacivanjeDTO
    {
        public String idStavke { get; set; }
        public String idProstorijeIzKojeSePrebacuje { get; set; }
        public String idProstorijeUKojuSePrebacuje { get; set; }
        public int kolicinaStavke { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumKraja { get; set; }

        public ProstorijaPrebacivanjeDTO(string idStavke, string idProstorijeIzKojeSePrebacuje, string idProstorijeUKojuSePrebacuje, int kolicinaStavke, DateTime datumPocetka, DateTime datumKraja)
        {
            this.idStavke = idStavke;
            this.idProstorijeIzKojeSePrebacuje = idProstorijeIzKojeSePrebacuje;
            this.idProstorijeUKojuSePrebacuje = idProstorijeUKojuSePrebacuje;
            this.kolicinaStavke = kolicinaStavke;
            this.datumPocetka = datumPocetka;
            this.datumKraja = datumKraja;
        }
    }
}
