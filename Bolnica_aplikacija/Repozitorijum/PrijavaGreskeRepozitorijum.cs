using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class PrijavaGreskeRepozitorijum
    {
        public List<String> ucitaj()
        {
            List<String> sveZalbe;
            try
            {
                sveZalbe = JsonSerializer.Deserialize<List<String>>(File.ReadAllText("Datoteke/Zalbe.txt"));
            }
            catch (Exception e)
            {
                sveZalbe = new List<String>();
            }

            return sveZalbe;
        }
        public void upisi(List<String> sveZalbe)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveZalbe, formatiranje);
            File.WriteAllText("Datoteke/Zalbe.txt", jsonString);
        }
    }
}
