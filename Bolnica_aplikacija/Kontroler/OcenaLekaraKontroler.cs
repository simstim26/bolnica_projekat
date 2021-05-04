using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class OcenaLekaraKontroler
    {
        public static List<OcenaLekara> ucitajSve()
        {
            return OcenaLekaraServis.getInstance().ucitajSve();
        }

        public static void dodajOcenu(OcenaLekara ocena)
        {
            OcenaLekaraServis.getInstance().dodajOcenu(ocena);
        }
    }
}
