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
        public static String[] prijava(String korisnickoIme, String lozinka)
        {
            return KorisnikServis.getInstance().prijava(korisnickoIme, lozinka);
        }

        public static void nadjiLekara(String idLekara)
        {
            KorisnikServis.getInstance().nadjiLekara(idLekara);
        }

        public static void NadjiPacijenta(String idPacijenta)
        {
            KorisnikServis.getInstance().NadjiPacijenta(idPacijenta);
        }

        public static void NadjiUpravnika(String idUpravnika)
        {
            KorisnikServis.getInstance().NadjiUpravnika(idUpravnika);
        }

        public static void nadjiSekretara(String idSekretara)
        {
            KorisnikServis.getInstance().nadjiSekretara(idSekretara);
        }

        public static Lekar getLekar()
        {
            return KorisnikServis.getInstance().getLekar();
        }

        public static Pacijent GetPacijent()
        {
            return KorisnikServis.getInstance().GetPacijent();
        }

        public static Upravnik GetUpravnik()
        {
            return KorisnikServis.getInstance().GetUpravnik();
        }

        public static Sekretar GetSekretar()
        {
            return KorisnikServis.getInstance().getSekretar();
        }

        public void dodajKorisnika(String id, String korisnickoIme, String lozinka, String tipKorisnika)
        {
            KorisnikServis.getInstance().dodajKorisnika(id, korisnickoIme, lozinka, tipKorisnika);
        }

    }
}
