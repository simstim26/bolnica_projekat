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
        private static TerminServis terminServis = new TerminServis();
        public static void nadjiPacijentaZaTermin(String idTermina)
        {
            terminServis.nadjiPacijentaZaTermin(idTermina);
        }

        public static void azuriranjeTerapijeZaTermin(String idTermina, String idTerapija)
        {
            terminServis.azuriranjeTerapijeZaTermin(idTermina ,idTerapija);
        }

        public static void azuriranjeIzvestajaZaTermin(String azuriraniIzvestaj, String idTermina)
        {
            terminServis.azuriranjeIzvestajaZaTermin(azuriraniIzvestaj, idTermina);
        }

        public static void dodavanjeIzvestajaZaTermin(String nazivBolesti,String izvestajSaTermina)
        {
            terminServis.dodavanjeIzvestajaZaTermin(nazivBolesti, izvestajSaTermina);
        }

        public static List<Prostorija> nadjiSlobodneProstorijeZaTermin(Lekar lekar, Termin termin)
        {
            return terminServis.nadjiSlobodneProstorijeZaTermin(lekar, termin);
        }
        public static int proveriDatumTermina(String idTermina)
        {
            Termin termin = terminServis.nadjiTerminPoId(idTermina);
            DateTime trenutanDatum = DateTime.Now.AddDays(1);
            return DateTime.Compare(termin.datum, trenutanDatum);
        }

        public static void promeniProstorijuTermina(String idTermina, String idProstorije)
        {
            terminServis.promeniProstorijuTermina(idTermina,idProstorije);
        }
        public static bool proveriTipTermina(Lekar ulogovaniLekar, String idTermina)
        {
            return terminServis.proveriTipTermina(ulogovaniLekar, idTermina);
        }
        public static void sacuvajTermin(String idTermina)
        {
            terminServis.sacuvajTermin(idTermina);
        }

        public static Termin getTermin()
        {
            return terminServis.getTermin();
        }

        public static String nadjiIdLekaraZaTermin(String idTermina)
        {
            return terminServis.nadjiIdLekaraZaTermin(idTermina);
        }

        public static List<Termin> ucitajSve()
        {
            return terminServis.ucitajSve();

        }
    }
}
