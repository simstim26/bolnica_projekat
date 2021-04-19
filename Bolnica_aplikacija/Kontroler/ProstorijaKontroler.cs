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
            return prostorijaServis.ucitajSve();
        }

        public static List<Prostorija> ucitajNeobrisane()
        {
            return prostorijaServis.ucitajNeobrisane();
        }

        public static void upisi(List<Prostorija> sveProstorije)
        {
            prostorijaServis.upisi(sveProstorije);
        }

        public static void NapraviProstoriju(Prostorija prostorija)
        {
            prostorijaServis.NapraviProstoriju(prostorija);
        }

        public static void AzurirajProstoriju(Prostorija prostorija)
        {
            prostorijaServis.AzurirajProstoriju(prostorija);
        }

        public static void ObrisiProstoriju(String idProstorija)
        {
            prostorijaServis.ObrisiProstoriju(idProstorija);
        }

        public static void dodajStavku(String prostorijaId, String stavkaId)
        {
            prostorijaServis.dodajStavku(prostorijaId, stavkaId);
        }
    }
}
