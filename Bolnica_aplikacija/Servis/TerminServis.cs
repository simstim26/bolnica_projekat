using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class TerminServis
    {
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private Termin termin; //lekar -> cuvanje izabranog termina (promena termina ili promena prostorije za izabrani termin

        public Termin nadjiTerminPoId(String idTermina)
        {
            Termin povratnaVrednost = null;
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    povratnaVrednost = termin;
                    break;
                }
            }

            return povratnaVrednost;
        }

        public bool proveriTipTermina(Lekar lekar, String idTermina)
        {
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    if (termin.tip == TipTermina.OPERACIJA && lekar.idSpecijalizacije != "0")
                    {
                        return true;
                    }
                }
            }

                return false;
        }
        public void sacuvajTermin(String idTermina)
        {
            termin = nadjiTerminPoId(idTermina);
        }

        public Termin getTermin()
        {
            return termin;
        }
    }
}
