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
    class LogovanjeRepozitorijum
    {
        public void dodajLogovanje(Logovanje logovanje)
        {
            var svaLogovanja = ucitajSve();
            svaLogovanja.Add(logovanje);
            upisi(svaLogovanja);
        }

        public List<Logovanje> ucitajSve()
        {
            List<Logovanje> svaLogovanja;
            try
            {
                svaLogovanja = JsonSerializer.Deserialize<List<Logovanje>>(File.ReadAllText("Datoteke/Logovanje.txt"));

            }
            catch (Exception e)
            {
                svaLogovanja = new List<Logovanje>();
            }

            return svaLogovanja;
        }

        public void upisi(List<Logovanje> svaLogovanja)
        {

            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(svaLogovanja, formatiranje);
            File.WriteAllText("Datoteke/Logovanje.txt", jsonString);
        }
    }
}
