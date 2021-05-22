using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    public class TerminDTO
    {
        public TipTermina tip { get; set; }
        public DateTime datum { get; set; }
        public DateTime satnica { get; set; }
        public bool jeZavrsen { get; set; }
        public String idTermina { get; set; }
        public String idProstorije { get; set; }
        public String idPacijenta { get; set; }
        public String idLekara { get; set; }
        public String idTerapije { get; set; }
        public String idBolesti { get; set; }
        public String izvestaj { get; set; }
        public String izvestajUputa { get; set; }
        public String idUputLekara { get; set; }
        public String idUputTermin { get; set; }

        public TipTermina tipUput { get; set; }
        public bool jeHitan { get; set; }

        public TerminDTO(DateTime datum, DateTime satnica, String idLekara, String idProstorije, TipTermina tip)
        {
            this.datum = datum;
            this.satnica = satnica;
            this.idLekara = idLekara;
            this.idProstorije = idProstorije;
            this.tip = tip;
        }
        public TerminDTO(TipTermina tip, DateTime datum, DateTime satnica, bool jeZavrsen, string idTermina, string idProstorije,
            string idPacijenta, string idLekara, string idTerapije, string idBolesti, string izvestaj, string izvestajUputa,
            string idUputLekara, string idUputTermin, TipTermina tipUput, bool jeHitan)
        {
            this.tip = tip;
            this.datum = datum;
            this.satnica = satnica;
            this.jeZavrsen = jeZavrsen;
            this.idTermina = idTermina;
            this.idProstorije = idProstorije;
            this.idPacijenta = idPacijenta;
            this.idLekara = idLekara;
            this.idTerapije = idTerapije;
            this.idBolesti = idBolesti;
            this.izvestaj = izvestaj;
            this.izvestajUputa = izvestajUputa;
            this.idUputLekara = idUputLekara;
            this.idUputTermin = idUputTermin;
            this.tipUput = tipUput;
            this.jeHitan = jeHitan;
        }

        public TerminDTO(Termin termin)
        {
            this.tip = termin.tip;
            this.datum = termin.datum;
            this.satnica = termin.satnica;
            this.jeZavrsen = termin.jeZavrsen;
            this.idTermina = termin.idTermina;
            this.idProstorije = termin.idProstorije;
            this.idPacijenta = termin.idPacijenta;
            this.idLekara = termin.idLekara;
            this.idTerapije = termin.idTerapije;
            this.idBolesti = termin.idBolesti;
            this.izvestaj = termin.izvestaj;
            this.izvestajUputa = termin.izvestajUputa;
            this.idUputLekara = termin.idUputLekara;
            this.idUputTermin = termin.idUputTermin;
            this.tipUput = termin.tipUput;
            this.jeHitan = termin.jeHitan;
        }

        public Termin konvertuj()
        {
            return new Termin(this);
        }

    }
}
