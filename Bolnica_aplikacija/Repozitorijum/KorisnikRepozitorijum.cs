using Bolnica_aplikacija.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija
{
    class KorisnikRepozitorijum
    {
        public List<PomocnaKlasaKorisnici> ucitajSve()
        {
            List<PomocnaKlasaKorisnici> sviKorisnici;
            try
            {
                sviKorisnici = JsonSerializer.Deserialize<List<PomocnaKlasaKorisnici>>(File.ReadAllText("Datoteke/Korisnici.txt"));
            }
            catch (Exception e)
            {
                sviKorisnici = new List<PomocnaKlasaKorisnici>();
            }

            return sviKorisnici;
        }

        public void dodajKorisnika(PomocnaKlasaKorisnici korisnik)
        {
            var sviKorisnici = ucitajSve();
            sviKorisnici.Add(korisnik);
            upisi(sviKorisnici);
        }

        public void azurirajKorisnika(PomocnaKlasaKorisnici korisnikZaAzuriranje)
        {
            var sviKorisnici = ucitajSve();
            
            foreach(PomocnaKlasaKorisnici korisnik in sviKorisnici)
            {
                if (korisnik.id.Equals(korisnikZaAzuriranje.id))
                {
                    korisnik.id = korisnikZaAzuriranje.id;
                    korisnik.korisnickoIme = korisnikZaAzuriranje.korisnickoIme;
                    korisnik.lozinka = korisnikZaAzuriranje.lozinka;
                    korisnik.tip = korisnikZaAzuriranje.tip;
                    break;
                }
            }
        }

        public void upisi(List<PomocnaKlasaKorisnici> sviKorisnici)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviKorisnici, formatiranje);
            File.WriteAllText("Datoteke/Korisnici.txt", jsonString);
        }
    }
}
