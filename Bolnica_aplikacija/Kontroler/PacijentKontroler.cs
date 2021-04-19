using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class PacijentKontroler
    {
        private static PacijentServis pacijentServis = new PacijentServis();

        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return pacijentServis.prikazPacijenata();
        }

        public static List<PacijentTermin> prikazPacijentovihTermina()
        {
            return pacijentServis.prikazPacijentovihTermina();
        }

        public static List<PacijentTermin> prikazSvihTerminaPacijenta()
        {
            return pacijentServis.prikazSvihTerminaPacijenta();
        }
        public static void zakaziTerminPacijentu(String idTermina)
        {
            pacijentServis.zakaziTerminPacijentu(idTermina);
        }

        public static void azurirajTerminPacijentu(String idStarogTermina, String idNovogTermina)
        {
            pacijentServis.azurirajTerminPacijentu(idStarogTermina, idNovogTermina);
        }
        public static void otkaziTerminPacijenta(String idTermina)
        {
            pacijentServis.otkaziTerminPacijenta(idTermina);
        }
        public static void nadjiPacijenta(String idPacijenta)
        {
            pacijentServis.nadjiPacijenta(idPacijenta);
        }

        public static Pacijent getPacijent()
        {
            return pacijentServis.getPacijent();
        }

        public static List<PacijentTermin> ucitajSlobodneTermine(int indikator, bool jeSekretar)
        {
            return pacijentServis.ucitajSlobodneTermine(indikator);
        }

    }
}
