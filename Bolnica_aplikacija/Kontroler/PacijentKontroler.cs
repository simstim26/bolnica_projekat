using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
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

        public static void sacuvajBolestTerapiju(BolestTerapija bolestTerapija)
        {
            pacijentServis.sacuvajBolestTerapiju(bolestTerapija);
        }

        public static List<BolestTerapija> ucitajSveTerapijeZaPacijenta()
        {
            return pacijentServis.ucitajSveTerapijeZaPacijenta();
        }

        public static BolestTerapija getBolestTerapija()
        {
            return pacijentServis.getBolestTerapija();
        }
        public static List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta()
        {
            return pacijentServis.nadjiIstorijuBolestiZaPacijenta();
        }


        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return pacijentServis.prikazPacijenata();
        }

        public static List<PacijentTermin> prikazPacijentovihTermina()
        {
            return pacijentServis.prikazPacijentovihTermina();
        }

        public static List<PacijentTermin> prikazBuducihTerminaPacijenta()
        {
            return pacijentServis.prikazBuducihTerminaPacijenta();
        }
        public static List<PacijentTermin> prikazProslihTerminaPacijenta()
        {
            return pacijentServis.prikazProslihTerminaPacijenta();
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
            return pacijentServis.ucitajSlobodneTermine(indikator, jeSekretar);
        }

        public static List<PacijentTermin> filtrirajTermine(int indikator, String kriterijum)
        {
            return pacijentServis.filtrirajTermine(indikator, kriterijum);
        }

        public static void napraviAlergiju(String idPacijenta, String nazivAlergije)
        {
            pacijentServis.napraviAlergiju(idPacijenta, nazivAlergije);
        }

        public static List<Alergija> procitajAlergije()
        {
           
            return pacijentServis.procitajAlergije();
        }

    }
}
