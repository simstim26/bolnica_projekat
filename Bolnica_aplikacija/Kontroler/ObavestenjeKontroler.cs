using Bolnica_aplikacija.Model;
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
        public static void napraviObavestenje(String naslovObavestenja, String sadrzajObavestenja)
        {
            obavestenjeServis.napraviObavestenje(naslovObavestenja, sadrzajObavestenja);
        }

        public static List<Obavestenje> ucitajObavestenja()
        {
            return obavestenjeServis.ucitajObavestenja();
        }

        public static void azurirajObavestenje(String id, String naslovObavestenja, String sadrzajObavestenja)
        {
            obavestenjeServis.azurirajObavestenje(id, naslovObavestenja, sadrzajObavestenja);
        }
    }
}
