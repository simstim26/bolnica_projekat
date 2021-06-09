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
    class AlergijaRepozitorijum
    {
        public void dodajAlergiju(Alergija alergija)
        {
            var sveAlergije = ucitajSve();
            sveAlergije.Add(alergija);
            upisi(sveAlergije);
        }
        public List<Alergija> ucitajSve()
        {
            List<Alergija> sveAlergije;
            try
            {
                sveAlergije = JsonSerializer.Deserialize<List<Alergija>>(File.ReadAllText("Datoteke/Alergije.txt"));
            }
            catch (Exception e)
            {
                sveAlergije = new List<Alergija>();
            }

            return sveAlergije;
        }

        public void upisi(List<Alergija> sveAlergije)
        {
            
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveAlergije, formatiranje);
            File.WriteAllText("Datoteke/Alergije.txt", jsonString);
        }

        public void azurirajAlergije(List<Alergija> alergije)
        {        
            upisi(alergije);
        }
    }
}
