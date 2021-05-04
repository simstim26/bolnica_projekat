using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class OcenaBolniceKontroler
    {
        public static List<OcenaBolnice> ucitajSve()
        {
            return OcenaBolniceServis.getInstance().ucitajSve();
        }

        public static void dodajOcenu(OcenaBolnice ocena)
        {
            OcenaBolniceServis.getInstance().dodajOcenu(ocena);
        }

    }
}
