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

        public static List<Notifikacija> prikazPacijentovihNotifikacija()
        {
            return notifikacijaServis.prikazPacijentovihNotifikacija();
        }

        public  static void azurirajNotifikaciju(Notifikacija notifikacija)
        {
            notifikacijaServis.azurirajNotifikaciju(notifikacija);
        }

        public static Notifikacija getNotifikacija(String idNotifikacije)
        {
            return notifikacijaServis.getNotifikacija(idNotifikacije);
        }
    }
}
