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
    class PacijentRepozitorijum
    {
        public List<Pacijent> ucitajSve()
        {
            List<Pacijent> sviPacijenti;
            try
            {
                sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/Pacijenti.txt"));
            }
            catch (Exception e)
            {
                sviPacijenti = new List<Pacijent>();
            }

            return sviPacijenti;
        }

        public void dodajPacijenta(Pacijent pacijent)
        {
            var sviPacijenti = ucitajSve();
            sviPacijenti.Add(pacijent);
            upisi(sviPacijenti);
        }

        public void azurirajPacijenta(Pacijent pacijentZaAzuriranje)
        {
            var sviPacijenti = ucitajSve();

            foreach (Pacijent pacijent in sviPacijenti)
            {
                if (pacijent.id.Equals(pacijentZaAzuriranje.id))
                {
                    pacijent.id = pacijentZaAzuriranje.id;
                    pacijent.korisnickoIme = pacijentZaAzuriranje.korisnickoIme;
                    pacijent.lozinka = pacijentZaAzuriranje.lozinka;
                    pacijent.adresa = pacijentZaAzuriranje.adresa;
                    pacijent.brojTelefona = pacijentZaAzuriranje.brojTelefona;
                    pacijent.datumRodjenja = pacijentZaAzuriranje.datumRodjenja;
                    pacijent.email = pacijentZaAzuriranje.email;
                    pacijent.idBolnice = pacijentZaAzuriranje.idBolnice;
                    pacijent.ime = pacijentZaAzuriranje.ime;
                    pacijent.prezime = pacijentZaAzuriranje.prezime;
                    pacijent.jeLogickiObrisan = pacijentZaAzuriranje.jeLogickiObrisan;
                    pacijent.jeGost = pacijentZaAzuriranje.jeGost;
                    pacijent.jmbg = pacijentZaAzuriranje.jmbg; 
                    break;
                }
            }
            upisi(sviPacijenti);
        }

        public void upisi(List<Pacijent> sviPacijenti)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviPacijenti, formatiranje);
            File.WriteAllText("Datoteke/Pacijenti.txt", jsonString);
        }
    }
}
