using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class OcenaBolniceServis
    {
        private static OcenaBolniceServis instance;
        private static OcenaBolniceRepozitorijum ocenaBolniceRepozitorijum = new OcenaBolniceRepozitorijum();

        public static OcenaBolniceServis getInstance()
        {
            if(instance == null)
            {
                instance = new OcenaBolniceServis();
            }

            return instance;
        }

        public List<OcenaBolnice> ucitajSve()
        {
            return ocenaBolniceRepozitorijum.ucitajSve();
        }

        public void dodajOcenu(OcenaBolnice ocena)
        {
            ocenaBolniceRepozitorijum.dodajOcenu(ocena);
        }
    }
}
