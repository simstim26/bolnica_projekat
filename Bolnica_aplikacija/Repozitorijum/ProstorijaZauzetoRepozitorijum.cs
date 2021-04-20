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
    class ProstorijaZauzetoRepozitorijum
    {
        public List<ProstorijaZauzeto> ucitajSve()
        {
            List<ProstorijaZauzeto> sveProstorije;
            try
            {
                sveProstorije = JsonSerializer.Deserialize<List<ProstorijaZauzeto>>(File.ReadAllText("Datoteke/ProstorijeZauzeto.txt"));
            }
            catch (Exception e)
            {
                sveProstorije = new List<ProstorijaZauzeto>();
            }

            return sveProstorije;
        }

        public void upisi(List<ProstorijaZauzeto> sveProstorije)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveProstorije, formatiranje);
            File.WriteAllText("Datoteke/ProstorijeZauzeto.txt", jsonString);
        }
    }
}
