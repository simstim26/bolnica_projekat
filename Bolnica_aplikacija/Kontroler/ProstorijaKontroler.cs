using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class ProstorijaKontroler
    {
        private static ProstorijaServis prostorijaServis = new ProstorijaServis();
        public static List<Prostorija> ucitajSve()
        {
            return ProstorijaServis.getInstance().ucitajSve();
        }

        public static List<Prostorija> pronadjiSlobodneBolnickeSobe()
        {
            return ProstorijaServis.getInstance().pronadjiSlobodneBolnickeSobe();
        }

        public static List<Prostorija> ucitajNeobrisane()
        {
            return ProstorijaServis.getInstance().ucitajNeobrisane();
        }

        public static void upisi(List<Prostorija> sveProstorije)
        {
            ProstorijaServis.getInstance().upisi(sveProstorije);
        }

        public static bool NapraviProstoriju(ProstorijaDTO prostorija)
        {
            return ProstorijaServis.getInstance().ProveriProstoriju(prostorija);
        }

        public static bool AzurirajProstoriju(ProstorijaDTO prostorija)
        {
            return ProstorijaServis.getInstance().AzurirajProstoriju(prostorija);
        }

        public static void ObrisiProstoriju(String idProstorija)
        {
            ProstorijaServis.getInstance().ObrisiProstoriju(idProstorija);
        }

        public static void dodajStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            ProstorijaServis.getInstance().dodajStavku(prebacivanje);
        }

        public static List<Stavka> dobaviStavkeIzProstorije(Prostorija prostorija)
        {
            return ProstorijaServis.getInstance().dobaviStavkeIzProstorije(prostorija);
        }

        public static void premestiStavku(ProstorijaPrebacivanjeDTO prebacivanje)
        {
            ProstorijaServis.getInstance().premestiStavku(prebacivanje);
        }

        public static Prostorija nadjiProstorijuPoId(String id)
        {
            return ProstorijaServis.getInstance().nadjiProstorijuPoId(id);
        }

        public static void zakaziRenoviranje(ProstorijaRenoviranje prostorija)
        {
            Prostorija prostorijaKojaSeRenovira = nadjiProstorijuPoId(prostorija.idProstorije);

            ProstorijaServis.getInstance().zakaziRenoviranje(prostorija);
            
        }

        public static void pregledajProstorijeZaRenoviranje()
        {
            ProstorijaServis.getInstance().pregledajProstorijeZaRenoviranje();
        }

        public static bool postojeTerminiZaPeriodPremestanja(DateTime datumPocetka, DateTime datumKraja, Prostorija p)
        {
            return ProstorijaServis.getInstance().postojeTerminiZaPeriodPremestanja(datumPocetka, datumKraja, p);
        }

        public static void azurirajBrojZauzetihKreveta(string id)
        {
            ProstorijaServis.getInstance().azurirajBrojZauzetihKreveta(id);
        }

        public static List<ProstorijeIzvestaj> pronadjiTermineZaSveProstorije(DateTime? datumPocetka, DateTime? datumKraja)
        {
            return ProstorijaServis.getInstance().pronadjiTermineZaSveProstorije(datumPocetka, datumKraja);
        }

        public static String postaviIDProstorija()
        {
            return ProstorijaServis.getInstance().postaviIDProstorija();
        }

        public static void noviBrojSobe(ProstorijaDTO prostorijaNova, Prostorija prostorija)
        {
            ProstorijaServis.getInstance().noviBrojSobe(prostorijaNova, prostorija);
        }
        public static void kopirajProstorijuIUpisi(Prostorija p)
        {
            ProstorijaServis.getInstance().kopirajProstorijuIUpisi(p);
        }

    }
}
