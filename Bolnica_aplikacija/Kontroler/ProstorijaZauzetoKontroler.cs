using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class ProstorijaZauzetoKontroler
    {
        private static ProstorijaZauzetoServis prostorijaZauzetoServis = new ProstorijaZauzetoServis();

        public static List<ProstorijaZauzeto> ucitajSve()
        {
            return prostorijaZauzetoServis.ucitajSve();
        }

        public static void upisi(List<ProstorijaZauzeto> sveProstorije)
        {
            prostorijaZauzetoServis.upisi(sveProstorije);
        }

        public static void zauzmiProstorije()
        {
            prostorijaZauzetoServis.zauzmiProstorije();
        }
    }
}
