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

        public static List<BolestTerapija> ucitajSveTerapijeZaPacijenta(String idPacijenta)
        {
            return pacijentServis.ucitajSveTerapijeZaPacijenta(idPacijenta);
        }

        public static List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta(String idPacijenta)
        {
            return pacijentServis.nadjiIstorijuBolestiZaPacijenta(idPacijenta);
        }


        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return pacijentServis.prikazPacijenata();
        }

        public static List<PacijentTermin> prikazPacijentovihTermina(String idPacijenta)
        {
            return pacijentServis.prikazPacijentovihTermina(idPacijenta);
        }

        public static List<PacijentTermin> prikazBuducihTerminaPacijenta(String idPacijenta)
        {
            return pacijentServis.prikazBuducihTerminaPacijenta(idPacijenta);
        }
        public static List<PacijentTermin> prikazProslihTerminaPacijenta(String idPacijenta)
        {
            return pacijentServis.prikazProslihTerminaPacijenta(idPacijenta);
        }
        public static void zakaziTerminPacijentu(String idPacijenta, String idTermina)
        {
            pacijentServis.zakaziTerminPacijentu(idPacijenta, idTermina);
        }

        public static void azurirajTerminPacijentu(String idStarogTermina, String idNovogTermina)
        {
            pacijentServis.azurirajTerminPacijentu(idStarogTermina, idNovogTermina);
        }
        public static void otkaziTerminPacijenta(String idTermina)
        {
            pacijentServis.otkaziTerminPacijenta(idTermina);
        }
        public static Pacijent nadjiPacijenta(String idPacijenta)
        {
           return pacijentServis.nadjiPacijenta(idPacijenta);
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

        public static List<Alergija> procitajAlergije(String idPacijenta)
        {
           
            return pacijentServis.procitajAlergije(idPacijenta);
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
