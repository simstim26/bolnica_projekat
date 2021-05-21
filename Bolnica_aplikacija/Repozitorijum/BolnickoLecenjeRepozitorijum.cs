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
    class BolnickoLecenjeRepozitorijum
    {

        public List<BolnickoLecenje> ucitajSve()
        {
            List<BolnickoLecenje> povratnaVrednost;

            try
            {
                JsonSerializerOptions option = new JsonSerializerOptions();
                option.Converters.Add(new KonverterBolnickoLecenje());
               povratnaVrednost = JsonSerializer.Deserialize<List<BolnickoLecenje>>(File.ReadAllText("Datoteke/BolnickoLecenje.txt"), option);
            }
            catch(Exception e)
            {
                povratnaVrednost = new List<BolnickoLecenje>();
            }

            return povratnaVrednost;
        }

        public void azurirajBolnickoLecenje(BolnickoLecenje bolnickoLecenjeZaAzuriranje)
        {
            List<BolnickoLecenje> sviUputi = ucitajSve();
            foreach(BolnickoLecenje bolnickoLecenje in sviUputi)
            {
                if (bolnickoLecenje.id.Equals(bolnickoLecenjeZaAzuriranje.id))
                {
                    bolnickoLecenje.kopiraj(bolnickoLecenjeZaAzuriranje);
                    break;
                }
            }

            upisi(sviUputi);
        }

        public void upisi(List<BolnickoLecenje> sviUputi)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            formatiranje.Converters.Add(new KonverterBolnickoLecenje());
            string jsonString = JsonSerializer.Serialize(sviUputi, formatiranje);
            File.WriteAllText("Datoteke/BolnickoLecenje.txt", jsonString);
            
        }
    }
}
