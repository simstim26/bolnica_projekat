using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    public class TerapijaDTO
    {
        public String id { get; set; }
        public String idLeka { get; set; }
        public String idPacijenta { get; set; }
        public String idBolesti { get; set; }
        public String idTermina { get; set; }
        public DateTime datumPocetka { get; set; }
        public int trajanje { get; set; }
        public String nacinUpotrebe { get; set; }

        public TerapijaDTO(string id, string idLeka, string idPacijenta, string idBolesti, string idTermina, DateTime datumPocetka, int trajanje, string nacinUpotrebe)
        {
            this.id = id;
            this.idLeka = idLeka;
            this.idPacijenta = idPacijenta;
            this.idBolesti = idBolesti;
            this.idTermina = idTermina;
            this.datumPocetka = datumPocetka;
            this.trajanje = trajanje;
            this.nacinUpotrebe = nacinUpotrebe;
        }
    }
}
