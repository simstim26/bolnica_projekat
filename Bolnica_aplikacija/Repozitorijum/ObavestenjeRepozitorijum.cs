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
    class ObavestenjeRepozitorijum
    {
        public void dodajObavestenje(Obavestenje obavestenje)
        {
            var svaObavestenja = ucitajSve();
            svaObavestenja.Add(obavestenje);
            upisi(svaObavestenja);
        }

        public List<Obavestenje> ucitajSve()
        {
            List<Obavestenje> svaObavestenja;
            try
            {
                svaObavestenja = JsonSerializer.Deserialize<List<Obavestenje>>(File.ReadAllText("Datoteke/Obavestenja.txt"));
            }
            catch (Exception e)
            {
                svaObavestenja = new List<Obavestenje>();
            }

            return svaObavestenja;
        }

        public void upisi(List<Obavestenje> svaObavestenja)
        {

            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(svaObavestenja, formatiranje);
            File.WriteAllText("Datoteke/Obavestenja.txt", jsonString);
        }

        public void azurirajObavestenje(Obavestenje obavestenjeZaAzuriranje)
        {
            var svaObavestenja = ucitajSve();

            foreach (Obavestenje obavestenje in svaObavestenja)
            {
                if (obavestenje.id.Equals(obavestenjeZaAzuriranje.id))
                {
                    obavestenje.id = obavestenjeZaAzuriranje.id;
                    obavestenje.naslovObavestenja = obavestenjeZaAzuriranje.naslovObavestenja;
                    obavestenje.sadrzajObavestenja = obavestenjeZaAzuriranje.sadrzajObavestenja;
                    obavestenje.notifikacija = obavestenjeZaAzuriranje.notifikacija;

                    break;
                }
            }
            upisi(svaObavestenja);
        }
    }
}
