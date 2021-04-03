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
    class LekarRepozitorijum
    {

        public List<Lekar> ucitajSve()
        {
            List<Lekar> sviLekari;
            try
            {
                sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/Lekari.txt"));
            }
            catch (Exception e)
            {
                sviLekari = new List<Lekar>();
            }

            return sviLekari;
        }
    }
}
