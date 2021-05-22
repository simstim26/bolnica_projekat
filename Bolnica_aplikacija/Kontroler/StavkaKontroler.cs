using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class StavkaKontroler
    {
        private static StavkaServis stavkaServis = new StavkaServis();

        public static List<Stavka> UcitajSve()
        {
            return StavkaServis.getInstance().UcitajSve();
        }

        public static List<Stavka> UcitajNeobrisaneStavke()
        {
            return StavkaServis.getInstance().UcitajNeobrisaneStavke();
        }

        public static void IzbrisiStavku(Stavka stavka)
        {
            StavkaServis.getInstance().IzbrisiStavku(stavka);
        }
        public static void Upisi(List<Stavka> sveStavke)
        {
            StavkaServis.getInstance().Upisi(sveStavke);
        }

        public static bool DodajStavku()
        {
            return StavkaServis.getInstance().DodajStavku();
        }

        public static bool IzmeniStavku(Stavka stavka)
        {
            return StavkaServis.getInstance().IzmeniStavku(stavka);
        }

        public static Stavka pronadjiStavkuPoId(String id)
        {
            return StavkaServis.getInstance().pronadjiStavkuPoId(id);
        }

        public static Stavka pronadjiStavkuIzProstorijePoId(Prostorija prostorija, String stavkaId)
        {
            return StavkaServis.getInstance().pronadjiStavkuIzProstorijePoId(prostorija, stavkaId);
        }

        public static List<Stavka> ucitajStatickeStavke()
        {
            return StavkaServis.getInstance().ucitajStatickeStavke();
        }

        public static List<Stavka> ucitajDinamickeStavke()
        {
            return StavkaServis.getInstance().ucitajDinamickeStavke();
        }

        public static List<Stavka> poredjajListuStavkiPoKoliciniRastuce(List<Stavka> stavke)
        {
            return StavkaServis.getInstance().poredjajListuStavkiPoKoliciniRastuce(stavke);
        }

        public static List<Stavka> poredjajListuStavkiPoKoliciniOpadajuce(List<Stavka> stavke)
        {
            return StavkaServis.getInstance().poredjajListuStavkiPoKoliciniOpadajuce(stavke);
        }

        public static List<Stavka> poredjajListuStavkiPoNazivuOpadajuce(List<Stavka> stavke)
        {
            return StavkaServis.getInstance().poredjajListuStavkiPoNazivuOpadajuce(stavke);
        }

        public static List<Stavka> poredjajListuStavkiPoNazivuRastuce(List<Stavka> stavke)
        {
            return StavkaServis.getInstance().poredjajListuStavkiPoNazivuRastuce(stavke);
        }
        public static List<Stavka> pretraziStavku(String kriterijum, List<Stavka> stavke)
        {
            return StavkaServis.getInstance().pretraziStavku(kriterijum, stavke);
        }

    }
}
