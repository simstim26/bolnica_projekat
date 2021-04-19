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
    class LekRepozitorijum
    {

        public List<Lek> ucitajSve()
        {
            List<Lek> sviLekovi;
            try
            {
                sviLekovi = JsonSerializer.Deserialize<List<Lek>>(File.ReadAllText("Datoteke/Lekovi.txt"));
            }
            catch (Exception e)
            {
                sviLekovi = new List<Lek>();
            }

            return sviLekovi;
        }

        public void upisi(List<Lek> sviLekovi)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviLekovi, formatiranje);
            File.WriteAllText("Datoteke/Lekovi.txt", jsonString);
        }
    }
}
