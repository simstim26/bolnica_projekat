using Bolnica_aplikacija.Interfejs.Implementacija;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class TerapijaRepozitorijum : IRepoImpl<Terapija>
    {
        public List<Terapija> ucitajSve()
        {
            return ucitajSve("Datoteke/Terapije.txt");
        }
    
        public void upisi(List<Terapija> sveTerapije)
        {
            upisi(sveTerapije, "Datoteke/Terapije.txt");
        }
    }
}
