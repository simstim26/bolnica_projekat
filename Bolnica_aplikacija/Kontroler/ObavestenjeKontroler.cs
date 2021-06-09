using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class ObavestenjeKontroler
    {
        private static ObavestenjeServis obavestenjeServis = new ObavestenjeServis();
        public static void napraviObavestenje(Obavestenje obavestenje)
        {
            obavestenjeServis.napraviObavestenje(obavestenje);
        }

        public static List<Obavestenje> ucitajObavestenja()
        {
            return obavestenjeServis.ucitajObavestenja();
        }

        public static void azurirajObavestenje(Obavestenje obavestenje)
        {
            obavestenjeServis.azurirajObavestenje(obavestenje);
        }

        public static void obrisiObavestenje(String idObavestenja)
        {
            obavestenjeServis.obrisiObavestenje(idObavestenja);
        }
    }
}
