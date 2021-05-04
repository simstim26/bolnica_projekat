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
        public static bool proveriStanjeAnkete(String idPacijenta)
        {
            return PacijentServis.getInstance().proveriStanjeAnkete(idPacijenta);
        }

        public static void postaviStanjeAnkete(String idPacijenta)
        {
            PacijentServis.getInstance().postaviStanjeAnkete(idPacijenta);
        }

        public static void pomeriTerminNaPrviSlobodan(String idPacijenta, String idTermina, String tip, String idSpecijalizacije)
        {
            PacijentServis.getInstance().pomeriTerminNaPrviSlobodan(idPacijenta, idTermina, tip, idSpecijalizacije);
        }

        public static void NapraviPacijenta(String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {
            PacijentServis.getInstance().NapraviPacijenta(idBolnice, gost, korisnickoIme, lozinka, jmbg, ime, prezime, datumRodj, adresa, email, telefon, alergije);
        }

        public static List<Pacijent> ProcitajPacijente()
        {
            return PacijentServis.getInstance().ProcitajPacijente();
        }

        public static void AzurirajPacijenta(String id, String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {
            PacijentServis.getInstance().AzurirajPacijenta(id, idBolnice, gost, korisnickoIme, lozinka, jmbg, ime, prezime, datumRodj, adresa, email, telefon, alergije);
        }

        public static void ObrisiPacijenta(String idPacijenta)
        {
            PacijentServis.getInstance().ObrisiPacijenta(idPacijenta);
        }

    }
}
