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
        public static List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            return LekServis.getInstance().ucitajSveSemTrenutnogNaTerapiji(idLeka);
        }
        public static Lek nadjiLekPoId(String idLeka)
        {
            return LekServis.getInstance().nadjiLekPoId(idLeka);
        }
        public static List<Lek> ucitajSve()
        {
            return LekServis.getInstance().ucitajSve();
        }
    }
}
