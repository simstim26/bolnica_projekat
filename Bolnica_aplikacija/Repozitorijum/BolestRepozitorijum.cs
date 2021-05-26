using Bolnica_aplikacija.Model;
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
    class BolestRepozitorijum
    {
        public List<Bolest> ucitajSve()
        {
            List<Bolest> sveBolesti;
            try
            {
                JsonSerializerOptions option = new JsonSerializerOptions();
                option.Converters.Add(new KonvertBolest());
                sveBolesti = JsonSerializer.Deserialize<List<Bolest>>(File.ReadAllText("Datoteke/Bolesti.txt"), option);
            }
            catch (Exception e)
            {
                sveBolesti = new List<Bolest>();
            }

            return sveBolesti;
        }

        public void azurirajBolest(Bolest bolestZaAzuriranje)
        {
            List<Bolest> sveBolesti = ucitajSve();
            foreach(Bolest bolest in sveBolesti)
            {
                if(bolest.id.Equals(bolestZaAzuriranje.id))
                {
                    bolest.kopiraj(bolestZaAzuriranje);
                    break;
                }
            }
            upisi(sveBolesti);
        }


        public void upisi(List<Bolest> sveBolesti)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            formatiranje.Converters.Add(new KonvertBolest());

            string jsonString = JsonSerializer.Serialize(sveBolesti, formatiranje);
            File.WriteAllText("Datoteke/Bolesti.txt", jsonString);
        }
    }
}
