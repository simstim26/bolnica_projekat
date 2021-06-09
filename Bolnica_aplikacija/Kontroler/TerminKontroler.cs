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
    class TerminKontroler
    {
        public static Pacijent nadjiPacijentaZaTermin(String idTermina)
        {
           return TerminServis.getInstance().nadjiPacijentaZaTermin(idTermina);
        }

        public static void azurirajUputTermina(String idTermina, String idUputTermina)
        {
            TerminServis.getInstance().azurirajUputTermina(idTermina, idUputTermina);
        }

        public static void azurirajIzvestajUputa(String idTermina, TipTermina tip, String izvestajUputa)
        {
            TerminServis.getInstance().azurirajIzvestajUputa(idTermina, tip, izvestajUputa);
        }

        public static void azurirajLekaraZaUput(String idTermina, String idUputLekara)
        {
            TerminServis.getInstance().azurirajLekaraZaUput(idTermina, idUputLekara);
        }

        public static void azurirajTermin(Termin terminZaAzuriranje)
        {
            TerminServis.getInstance().azurirajTermin(terminZaAzuriranje);
        }

        public static void azuriranjeTerapijeZaTermin(String idTermina, String idTerapija)
        {
            TerminServis.getInstance().azuriranjeTerapijeZaTermin(idTermina ,idTerapija);
        }

        public static void azuriranjeIzvestajaZaTermin(String azuriraniIzvestaj, String idTermina)
        {
            TerminServis.getInstance().azuriranjeIzvestajaZaTermin(azuriraniIzvestaj, idTermina);
        }

        public static void dodavanjeIzvestajaZaTermin(String idTermina, String nazivBolesti,String izvestajSaTermina)
        {
            TerminServis.getInstance().dodavanjeIzvestajaZaTermin(idTermina, nazivBolesti, izvestajSaTermina);
        }

        public static List<Prostorija> nadjiSlobodneProstorijeZaTermin(Lekar lekar, TerminDTO termin)
        {
            return TerminServis.getInstance().nadjiSlobodneProstorijeZaTermin(lekar, new Termin(termin));
        }
        public static int proveriDatumTermina(String idTermina)
        {
            Termin termin = TerminServis.getInstance().nadjiTerminPoId(idTermina);
            DateTime trenutanDatum = DateTime.Now.AddDays(1);
            return DateTime.Compare(termin.datum, trenutanDatum);
        }

        public static void promeniProstorijuTermina(String idTermina, String idProstorije)
        {
            TerminServis.getInstance().promeniProstorijuTermina(idTermina,idProstorije);
        }
        public static bool proveriTipTermina(Lekar ulogovaniLekar, String idTermina)
        {
            return TerminServis.getInstance().proveriTipTermina(ulogovaniLekar, idTermina);
        }

        public static String nadjiIdLekaraZaTermin(String idTermina)
        {
            return TerminServis.getInstance().nadjiIdLekaraZaTermin(idTermina);
        }

        public static List<Termin> ucitajSve()
        {
            return TerminServis.getInstance().ucitajSve();

        }

        public static List<PacijentTermin> ucitajPregledaZaIzabranogLekara(String idLekara)
        {
            return TerminServis.getInstance().ucitajPregledaZaIzabranogLekara(idLekara);
        }

        public static String napraviTermin(TerminDTO termin)
        {
            return TerminServis.getInstance().napraviTermin(new Termin(termin));
        }

        public static Termin nadjiTerminPoId(String idTermina)
        {
            return TerminServis.getInstance().nadjiTerminPoId(idTermina);
        }

        public static List<PacijentTermin> ucitajTermineZaHitanSlucaj(String tip, String idSpecijalizacije)
        {
            return TerminServis.getInstance().ucitajTermineZaHitanSlucaj(tip, idSpecijalizacije);
        }

        public static void oznaciHitanSlucaj(String idTermina)
        {
            TerminServis.getInstance().oznaciHitanTermin(idTermina);
        }

        public static List<Termin> pronadjiTermineZaIzvestajSekretara(DateTime pocetak, DateTime kraj)
        {
            return TerminServis.getInstance().pronadjiTermineZaIzvestajSekretara(pocetak, kraj);
        }
    
        public static List<PacijentTermin> pronadjiPacijentTerminUTrenutnomMesecu(String idPacijenta)
        {
            return TerminServis.getInstance().pronadjiPacijentTerminUTrenutnomMesecu(idPacijenta);
        }
    
        public static int pronadjiOdradjeneTermineZaMesec(String idPacijenta, int mesec)
        {
            return TerminServis.getInstance().pronadjiOdradjeneTermineZaMesec(idPacijenta, mesec);
        }

        public static List<PacijentTermin> pronadjiOdradjeneTerminePacijenta(String idPacijenta)
        {
            return TerminServis.getInstance().pronadjiOdradjeneTerminePacijenta(idPacijenta);
        }
    }
}
