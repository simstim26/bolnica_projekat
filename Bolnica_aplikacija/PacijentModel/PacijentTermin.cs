using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentModel
{
    public class PacijentTermin
    {
        private DateTime datum;
        private DateTime satnica;
        private String nazivProstorije;
        private String napomena;
        private String imeLekara;


        public DateTime getDatum()
        {
            return datum;
        }

        public DateTime getSatnica()
        {
            return satnica;
        }

        public String getNazivProstorije()
        {
            return nazivProstorije;
        }

        public String getNapomena()
        {
            return napomena;
        }

        public String getImeLekara()
        {
            return imeLekara;
        }

        public void setDatum(DateTime datum)
        {
            this.datum = datum;
        }

        public void setSatnica(DateTime satnica)
        {
            this.satnica = satnica;
        }

        public void setNazivProstorije(String nazivProstorije)
        {
            this.nazivProstorije = nazivProstorije;
        }

        public void setNapomena(String napomena)
        {
            this.napomena = napomena;
        }

        public void setImeLekara(String imeLekara)
        {
            this.imeLekara = imeLekara;
        }
    }
}
