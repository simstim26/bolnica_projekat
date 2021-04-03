using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class KorisnikKontroler
    {
        private static KorisnikServis korisnikServis = new KorisnikServis();
        public static String[] prijava(String korisnickoIme, String lozinka)
        {
            return korisnikServis.prijava(korisnickoIme, lozinka);
        }

        public static void nadjiLekara(String idLekara)
        {
            korisnikServis.nadjiLekara(idLekara);
        }

        public static void NadjiPacijenta(String idPacijenta)
        {
            korisnikServis.NadjiPacijenta(idPacijenta);
        }

        public static Lekar getLekar()
        {
            return korisnikServis.getLekar();
        }

        public static Pacijent GetPacijent()
        {
            return korisnikServis.GetPacijent();
        }

    }
}
