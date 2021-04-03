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
    class TerminRepozitorijum
    {

        public List<Termin> ucitajSve()
        {
            List<Termin> sviTermini;
            try
            {
                sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));
            }
            catch(Exception e)
            {
                sviTermini = new List<Termin>();
            }
    
            return sviTermini;
        }

        public void dodajTermin(Termin termin) //da li ide u servis???
        {
            var sviTermini = ucitajSve();
            sviTermini.Add(termin);
            upisi(sviTermini);
        }

        public void azurirajTermin(Termin terminZaAzuriranje) //da li ide u servis ??
        {
            var sviTermini = ucitajSve();
            foreach(Termin termin in sviTermini)
            {
                if (termin.idTermina.Equals(terminZaAzuriranje.idTermina))
                {
                    termin.idLekara = terminZaAzuriranje.idLekara;
                    termin.idPacijenta = terminZaAzuriranje.idPacijenta;
                    termin.idProstorije = terminZaAzuriranje.idProstorije;
                    termin.jeZavrsen = terminZaAzuriranje.jeZavrsen;
                    termin.datum = terminZaAzuriranje.datum;
                    termin.satnica = terminZaAzuriranje.satnica;
                    break;
                }
            }

            upisi(sviTermini);
        }

        public void upisi(List<Termin> sviTermini)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviTermini, formatiranje);
            File.WriteAllText("Datoteke/Termini.txt", jsonString);
        }
    }
}
