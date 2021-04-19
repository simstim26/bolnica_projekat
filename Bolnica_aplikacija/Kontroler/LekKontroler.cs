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

        public static List<Lek> ucitajSve()
        {
            return lekServis.ucitajSve();
        }
    }
}
