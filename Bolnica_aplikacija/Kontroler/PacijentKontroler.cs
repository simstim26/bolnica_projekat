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
            PacijentServis.getInstance().sacuvajBolestTerapiju(bolestTerapija);
        }

        public static List<BolestTerapija> ucitajSveTerapijeZaPacijenta()
        {
            return PacijentServis.getInstance().ucitajSveTerapijeZaPacijenta();
        }

        public static BolestTerapija getBolestTerapija()
        {
            return PacijentServis.getInstance().getBolestTerapija();
        }
        public static List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta()
        {
            return PacijentServis.getInstance().nadjiIstorijuBolestiZaPacijenta();
        }


        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return PacijentServis.getInstance().prikazPacijenata();
        }

        public static List<PacijentTermin> prikazPacijentovihTermina()
        {
            return PacijentServis.getInstance().prikazPacijentovihTermina();
        }

        public static List<PacijentTermin> prikazBuducihTerminaPacijenta()
        {
            return PacijentServis.getInstance().prikazBuducihTerminaPacijenta();
        }
        public static List<PacijentTermin> prikazProslihTerminaPacijenta()
        {
            return PacijentServis.getInstance().prikazProslihTerminaPacijenta();
        }
        public static void zakaziTerminPacijentu(String idTermina)
        {
            PacijentServis.getInstance().zakaziTerminPacijentu(idTermina);
        }

        public static void azurirajTerminPacijentu(String idStarogTermina, String idNovogTermina)
        {
            PacijentServis.getInstance().azurirajTerminPacijentu(idStarogTermina, idNovogTermina);
        }
        public static void otkaziTerminPacijenta(String idTermina)
        {
            PacijentServis.getInstance().otkaziTerminPacijenta(idTermina);
        }
        public static void nadjiPacijenta(String idPacijenta)
        {
            PacijentServis.getInstance().nadjiPacijenta(idPacijenta);
        }

        public static Pacijent getPacijent()
        {
            return PacijentServis.getInstance().getPacijent();
        }

        public static List<PacijentTermin> ucitajSlobodneTermine(int indikator, bool jeSekretar)
        {
            return PacijentServis.getInstance().ucitajSlobodneTermine(indikator, jeSekretar);
        }

        public static List<PacijentTermin> filtrirajTermine(int indikator, String kriterijum)
        {
            return PacijentServis.getInstance().filtrirajTermine(indikator, kriterijum);
        }

        public static void napraviAlergiju(String idPacijenta, String nazivAlergije)
        {
            PacijentServis.getInstance().napraviAlergiju(idPacijenta, nazivAlergije);
        }

        public static List<Alergija> procitajAlergije()
        {
           
            return PacijentServis.getInstance().procitajAlergije();
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
