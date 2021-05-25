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

        public void upisi(List<Lekar> sviLekari)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviLekari, formatiranje);
            File.WriteAllText("Datoteke/Lekari.txt", jsonString);
        }

        public void dodajLekara(Lekar lekar)
        {
            List<Lekar> sviLekari = ucitajSve();
            sviLekari.Add(lekar);
            upisi(sviLekari);
        }
     
    }
}
