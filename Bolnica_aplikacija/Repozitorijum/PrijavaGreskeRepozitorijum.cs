using Bolnica_aplikacija.Interfejs.Implementacija;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class PrijavaGreskeRepozitorijum : IRepoImpl<String>
    {
        public List<String> ucitaj()
        {
            return ucitajSve("Datoteke/Zalbe.txt");
        }
        public void upisi(List<String> sveZalbe)
        {
            upisi(sveZalbe, "Datoteke/Zalbe.txt");
        }
    }
}
