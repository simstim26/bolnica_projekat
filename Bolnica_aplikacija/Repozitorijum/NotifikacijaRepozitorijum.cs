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
    class NotifikacijaRepozitorijum
    {
        public List<Notifikacija> ucitajSve()
        {
            List<Notifikacija> sveNotifikacije;

            try
            {
                sveNotifikacije = JsonSerializer.Deserialize<List<Notifikacija>>(File.ReadAllText("Datoteke/Notifikacije.txt"));

            }
            catch (Exception e)
            {
                sveNotifikacije = new List<Notifikacija>();
            }

            return sveNotifikacije;

        }

        public void dodajNotifikaciju(Notifikacija notifikacija)
        {
            var sveNotifikacije = ucitajSve();
            sveNotifikacije.Add(notifikacija);
            upisi(sveNotifikacije);
        }

        public void azurirajNotifikaciju(Notifikacija notifikacijaZaAzuriranje)
        {
            var sveNotifikacije = ucitajSve();

            foreach(Notifikacija notifikacija in sveNotifikacije)
            {
                if(notifikacija.id.Equals(notifikacijaZaAzuriranje.id))
                {
                    notifikacija.id = notifikacijaZaAzuriranje.id;
                    notifikacija.idKorisnika = notifikacijaZaAzuriranje.idKorisnika;
                    notifikacija.nazivNotifikacije = notifikacijaZaAzuriranje.nazivNotifikacije;
                    notifikacija.porukaNotifikacije = notifikacijaZaAzuriranje.porukaNotifikacije;
                    notifikacija.vremeNotifikovanja = notifikacijaZaAzuriranje.vremeNotifikovanja;
                    notifikacija.datumNotifikovanja = notifikacijaZaAzuriranje.datumNotifikovanja;
                    notifikacija.ponavljanje = notifikacijaZaAzuriranje.ponavljanje;

                    break;
                }
            }

            upisi(sveNotifikacije);

        }

        public void upisi(List<Notifikacija> sveNotifikacije)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            string jsonString = JsonSerializer.Serialize(sveNotifikacije, formatiranje);
            File.WriteAllText("Datoteke/Notifikacije.txt", jsonString);

        } 
            
            
        
    }
}
