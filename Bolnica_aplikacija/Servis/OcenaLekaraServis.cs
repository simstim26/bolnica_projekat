using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class OcenaLekaraServis
    {
        private static OcenaLekaraServis instance;
        private static OcenaLekaraRepozitorijum ocenaLekaraRepozitorijum = new OcenaLekaraRepozitorijum();

        public static OcenaLekaraServis getInstance()
        {
            if(instance == null)
                instance = new OcenaLekaraServis();

            return instance;
        }
        public List<OcenaLekara> ucitajSve()
        {
            return ocenaLekaraRepozitorijum.ucitajSve();
        }

        public void dodajOcenu(OcenaLekara ocena)
        {
            ocenaLekaraRepozitorijum.dodajOcenu(ocena);
        }
    }
}
