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
    class TerminKontroler
    {
        public static Pacijent nadjiPacijentaZaTermin(String idTermina)
        {
           return TerminServis.getInstance().nadjiPacijentaZaTermin(idTermina);
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

        public static void dodavanjeIzvestajaZaTermin(String nazivBolesti,String izvestajSaTermina)
        {
            TerminServis.getInstance().dodavanjeIzvestajaZaTermin(nazivBolesti, izvestajSaTermina);
        }

        public static List<Prostorija> nadjiSlobodneProstorijeZaTermin(Lekar lekar, Termin termin)
        {
            return TerminServis.getInstance().nadjiSlobodneProstorijeZaTermin(lekar, termin);
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
        public static void sacuvajTermin(String idTermina)
        {
            TerminServis.getInstance().sacuvajTermin(idTermina);
        }

        public static Termin getTermin()
        {
            return TerminServis.getInstance().getTermin();
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

        public static String napraviTermin(Termin termin)
        {
            return TerminServis.getInstance().napraviTermin(termin);
        }

        public static void veziTermin(String idTerminUput)
        {
            TerminServis.getInstance().veziTermin(idTerminUput);
        }

        public static Termin nadjiTerminPoId(String idTermina)
        {
            return TerminServis.getInstance().nadjiTerminPoId(idTermina);
        }

        public static List<PacijentTermin> ucitajTermineZaHitanSlucaj(String tip, String idSpecijalizacije)
        {
            return TerminServis.getInstance().ucitajTermineZaHitanSlucaj(tip, idSpecijalizacije);
        }
    }
}
