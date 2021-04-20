using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class LekKontroler
    {
        private static LekServis lekServis = new LekServis();

        public static List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            return lekServis.ucitajSveSemTrenutnogNaTerapiji(idLeka);
        }
        public static Lek nadjiLekPoId(String idLeka)
        {
            return lekServis.nadjiLekPoId(idLeka);
        }
        public static List<Lek> ucitajSve()
        {
            return lekServis.ucitajSve();
        }
    }
}
