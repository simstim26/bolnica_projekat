using Bolnica_aplikacija.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class OcenaBolniceRepozitorijum
    {
        public void dodajOcenu(OcenaBolnice ocena)
        {
            var sveOcene = ucitajSve();
            sveOcene.Add(ocena);
            upisi(sveOcene);
        }

        public List<OcenaBolnice> ucitajSve()
        {
            List<OcenaBolnice> sveOcene;
            try
            {
                sveOcene = JsonSerializer.Deserialize<List<OcenaBolnice>>(File.ReadAllText("Datoteke/OceneBolnice.txt"));
            }
            catch (Exception e)
            {
                sveOcene = new List<OcenaBolnice>();
            }

            return sveOcene;
        }

        public void upisi(List<OcenaBolnice> sveOcene)
        {

            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveOcene, formatiranje);
            File.WriteAllText("Datoteke/OceneBolnice.txt", jsonString);
        }

    }
}
