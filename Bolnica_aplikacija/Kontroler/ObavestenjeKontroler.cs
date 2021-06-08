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
        public static void napraviObavestenje(ObavestenjeDTO obavestenjeDTO)
        {
            Obavestenje obavestenje = new Obavestenje(obavestenjeDTO.naslovObavestenja, obavestenjeDTO.sadrzajObavestenja);
            obavestenjeServis.napraviObavestenje(obavestenje);
        }

        public static List<Obavestenje> ucitajObavestenja()
        {
            return obavestenjeServis.ucitajObavestenja();
        }

        public static void azurirajObavestenje(ObavestenjeDTO obavestenjeDTO)
        {
            Obavestenje obavestenjeIzmena = new Obavestenje(obavestenjeDTO.naslovObavestenja, obavestenjeDTO.sadrzajObavestenja);
            obavestenjeIzmena.id = obavestenjeDTO.id;
            Console.WriteLine("ID: " + obavestenjeDTO.id);
            obavestenjeServis.azurirajObavestenje(obavestenjeIzmena);
        }

        public static void obrisiObavestenje(String idObavestenja)
        {
            obavestenjeServis.obrisiObavestenje(idObavestenja);
        }
    }
}
