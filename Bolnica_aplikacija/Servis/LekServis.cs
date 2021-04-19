using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class LekServis
    {
        private LekRepozitorijum lekRepozitorijum = new LekRepozitorijum();

        public List<Lek> ucitajSve()
        {
            return lekRepozitorijum.ucitajSve();
        }

    }
}
