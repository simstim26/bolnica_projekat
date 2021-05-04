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
    class OcenaLekaraRepozitorijum
    {
        public void dodajOcenu(OcenaLekara ocena)
        {
            var sveOcene = ucitajSve();
            sveOcene.Add(ocena);
            upisi(sveOcene);
        }

        public List<OcenaLekara> ucitajSve()
        {
            List<OcenaLekara> sveOcene;
            try
            {
                sveOcene = JsonSerializer.Deserialize<List<OcenaLekara>>(File.ReadAllText("Datoteke/OceneLekara.txt"));
            }
            catch (Exception e)
            {
                sveOcene = new List<OcenaLekara>();
            }

            return sveOcene;
        }

        public void upisi(List<OcenaLekara> sveOcene)
        {

            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveOcene, formatiranje);
            File.WriteAllText("Datoteke/OceneLekara.txt", jsonString);
        }

    }
}
