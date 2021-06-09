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
    class SpecijalizacijaRepozitorijum : IRepoImpl<Specijalizacija>
    {
        public List<Specijalizacija> ucitajSve()
        {
            return ucitajSve("Datoteke/Specijalizacije.txt");
        }

        public void upisi(List<Specijalizacija> sveSpecijalizacije)
        {
            upisi(sveSpecijalizacije, "Datoteke/Specijalizacije.txt");
        }
    }
}
