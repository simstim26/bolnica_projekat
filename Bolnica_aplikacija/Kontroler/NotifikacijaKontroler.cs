using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class NotifikacijaKontroler
    {
        private static NotifikacijaServis notifikacijaServis = new NotifikacijaServis();

        public static List<Notifikacija> prikazPacijentovihNotifikacija(String idKorisnika)
        {
            return notifikacijaServis.prikazPacijentovihNotifikacija(idKorisnika);
        }

        public  static void azurirajNotifikaciju(Notifikacija notifikacija)
        {
            notifikacijaServis.azurirajNotifikaciju(notifikacija);
        }

        public static Notifikacija getNotifikacija(String idNotifikacije, String idKorisnika)
        {
            return notifikacijaServis.getNotifikacija(idNotifikacije, idKorisnika);
        }

        public static bool proveriVreme(String trenutnoVreme, String trenutanDatum, String idKorisnika)
        {
            return notifikacijaServis.proveriVreme(trenutnoVreme, trenutanDatum, idKorisnika);
        }

        public static List<Notifikacija> getNoveNotifikacijeKorisnika(String idKorisnika)
        {
            return notifikacijaServis.getNoveNotifikacijeKorisnika(idKorisnika);
        }

        public static void procitajNotifikaciju(String idNotifikacije, String idKorisnika)
        {
            notifikacijaServis.procitajNotifikaciju(idNotifikacije, idKorisnika);
        }

        public static void napraviNotifikaciju(String nazivNotifikacije, String porukaNotifikacije, String idKorisnika, String tipKorisnika)
        {
            notifikacijaServis.napraviNotifikaciju(nazivNotifikacije, porukaNotifikacije, idKorisnika, tipKorisnika);
        }
    }
}
