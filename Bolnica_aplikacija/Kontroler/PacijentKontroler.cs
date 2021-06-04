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

        public static bool proveriAlergijuNaLekZaPacijenta(String idPacijenta, String idIzabraniLek)
        {
            return PacijentServis.getInstance().proveriAlergijuNaLekZaPacijenta(idPacijenta, idIzabraniLek);
        }
        public static List<BolestTerapija> ucitajSveTerapijeZaPacijenta(String idPacijenta)
        {
            return PacijentServis.getInstance().ucitajSveTerapijeZaPacijenta(idPacijenta);
        }
        
        public static List<PacijentTermin> pretraziProsleTermineZaPacijenta(String idPacijenta, DateTime prvi, DateTime drugi)
        {
            return PacijentServis.getInstance().pretraziProsleTermineZaPacijenta(idPacijenta, prvi, drugi);
        }


        public static List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta(String idPacijenta)
        {
            return PacijentServis.getInstance().nadjiIstorijuBolestiZaPacijenta(idPacijenta);
        }


        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return PacijentServis.getInstance().prikazPacijenata();
        }

        public static List<PacijentTermin> prikazPacijentovihTermina(String idPacijenta)
        {
            return PacijentServis.getInstance().prikazPacijentovihTermina(idPacijenta);
        }

        public static List<PacijentTermin> prikazBuducihTerminaPacijenta(String idPacijenta)
        {
            return PacijentServis.getInstance().prikazBuducihTerminaPacijenta(idPacijenta);
        }
        public static List<PacijentTermin> prikazProslihTerminaPacijenta(String idPacijenta)
        {
            return PacijentServis.getInstance().prikazProslihTerminaPacijenta(idPacijenta);
        }
        public static void zakaziTerminPacijentu(String idPacijenta, String idTermina)
        {
            PacijentServis.getInstance().zakaziTerminPacijentu(idPacijenta, idTermina);
        }

        public static void azurirajTerminPacijentu(String idStarogTermina, String idNovogTermina)
        {
            PacijentServis.getInstance().azurirajTerminPacijentu(idStarogTermina, idNovogTermina);
        }
        public static void otkaziTerminPacijenta(String idTermina)
        {
            PacijentServis.getInstance().otkaziTerminPacijenta(idTermina);
        }
        public static Pacijent nadjiPacijenta(String idPacijenta)
        {
           return PacijentServis.getInstance().nadjiPacijenta(idPacijenta);
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

        public static List<Alergija> procitajAlergije(String idPacijenta)
        {
           
            return PacijentServis.getInstance().procitajAlergije(idPacijenta);
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

        public static void NapraviPacijenta(PacijentDTO pacijentDTO, List<Alergija> alergije)
        {
            Pacijent pacijent = new Pacijent(pacijentDTO.idBolnice, pacijentDTO.jeGost, pacijentDTO.korisnickoIme, pacijentDTO.lozinka, pacijentDTO.jmbg, pacijentDTO.ime, pacijentDTO.prezime, pacijentDTO.datumRodjenja, pacijentDTO.adresa, pacijentDTO.email, pacijentDTO.brojTelefona);
            PacijentServis.getInstance().NapraviPacijenta(pacijent,alergije);
        }

        public static List<Pacijent> ProcitajPacijente()
        {
            return PacijentServis.getInstance().ProcitajPacijente();
        }

        public static void AzurirajPacijenta(PacijentDTO pacijentDTO, List<Alergija> alergije)
        {
            Pacijent pacijent = new Pacijent(pacijentDTO.idBolnice, pacijentDTO.jeGost, pacijentDTO.korisnickoIme, pacijentDTO.lozinka, pacijentDTO.jmbg, pacijentDTO.ime, pacijentDTO.prezime, pacijentDTO.datumRodjenja, pacijentDTO.adresa, pacijentDTO.email, pacijentDTO.brojTelefona);
            pacijent.id = pacijentDTO.id;
            PacijentServis.getInstance().AzurirajPacijenta(pacijent, alergije);
        }

        public static void ObrisiPacijenta(String idPacijenta)
        {
            PacijentServis.getInstance().ObrisiPacijenta(idPacijenta);
        }

        public static List<TerapijaPacijent> ucitajAktivneTerapije(String idPacijenta)
        {
            return PacijentServis.getInstance().ucitajAktivneTerapije(idPacijenta);
        }

    }
}
