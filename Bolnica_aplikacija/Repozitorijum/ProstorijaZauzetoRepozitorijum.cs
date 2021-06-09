using Bolnica_aplikacija.Interfejs.Implementacija;
using Bolnica_aplikacija.PomocneKlase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class ProstorijaZauzetoRepozitorijum : IRepoImpl<ProstorijaZauzeto>
    {
        public List<ProstorijaZauzeto> ucitajSve()
        {
            return ucitajSve("Datoteke/ProstorijeZauzeto.txt");
        }

        public void upisi(List<ProstorijaZauzeto> sveProstorije)
        {
            upisi(sveProstorije, "Datoteke/ProstorijeZauzeto.txt");
        }
    }
}
