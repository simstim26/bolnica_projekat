using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class TerminKontroler
    {
        private static TerminServis terminServis = new TerminServis();

        public static int proveriDatumTermina(String idTermina)
        {
            Termin termin = terminServis.nadjiTerminPoId(idTermina);
            DateTime trenutanDatum = DateTime.Now.AddDays(1);
            return DateTime.Compare(termin.datum, trenutanDatum);
        }

        public static bool proveriTipTermina(Lekar ulogovaniLekar, String idTermina)
        {
            return terminServis.proveriTipTermina(ulogovaniLekar, idTermina);
        }
        public static void sacuvajTermin(String idTermina)
        {
            terminServis.sacuvajTermin(idTermina);
        }

        public static Termin getTermin()
        {
            return terminServis.getTermin();
        }
    }
}
