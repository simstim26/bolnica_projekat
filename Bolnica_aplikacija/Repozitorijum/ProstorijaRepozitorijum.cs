using Bolnica_aplikacija.PomocneKlase;
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
    class ProstorijaRepozitorijum
    {

        public List<Prostorija> ucitajSve()
        {
            List<Prostorija> sveProstorije;
            try
            {
                sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/Prostorije.txt"));
            }
            catch (Exception e)
            {
                sveProstorije = new List<Prostorija>();
            }

            return sveProstorije;
        }

        public List<Prostorija> ucitajNeobrisane()
        {
            List<Prostorija> sveProstorije = ucitajSve();
            List<Prostorija> neobrisaneProstorije = new List<Prostorija>();

            foreach (Prostorija p in sveProstorije)
            {
                if (p.logickiObrisana == false)
                {
                    neobrisaneProstorije.Add(p);
                }
            }

            return neobrisaneProstorije;
        }

        public void dodajProstoriju(Prostorija prostorija) //da li ide u servis???
        {
            var sveProstorije = ucitajSve();
            sveProstorije.Add(prostorija);
            upisi(sveProstorije);
        }

        public void azurirajProstoriju(Prostorija prostorijaZaAzuriranje) //da li ide u servis ??
        {
            var sveProstorije = ucitajSve();
            foreach (Prostorija prostorija in sveProstorije)
            {
                if (prostorija.id.Equals(prostorijaZaAzuriranje.id))
                {
                    prostorija.id = prostorijaZaAzuriranje.id;
                    prostorija.idBolnice = prostorijaZaAzuriranje.idBolnice;
                    prostorija.logickiObrisana = prostorijaZaAzuriranje.logickiObrisana;
                    prostorija.sprat = prostorija.sprat;
                    prostorija.tipProstorije = prostorijaZaAzuriranje.tipProstorije;
                    prostorija.broj = prostorijaZaAzuriranje.broj;
                    prostorija.dostupnost = prostorijaZaAzuriranje.dostupnost;
                    break;
                }
            }

            upisi(sveProstorije);
        }

        public void upisi(List<Prostorija> sveProstorije)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveProstorije, formatiranje);
            File.WriteAllText("Datoteke/Prostorije.txt", jsonString);
        }

        public Dictionary<string, string> prostorijaBrojSprat()
        {
            var neobrisaneProstorije = ucitajNeobrisane();
            Dictionary<string, string> prostorije = new Dictionary<string, string>();

            foreach (Prostorija p in neobrisaneProstorije)
            {
                prostorije.Add(p.id, p.broj + " " + p.sprat);
            }

            return prostorije;
        }

        public void upisiProstorijeZaRenoviranje(List<ProstorijaRenoviranje> prostorijeZaRenoviranje)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(prostorijeZaRenoviranje, formatiranje);
            File.WriteAllText("Datoteke/ProstorijeRenoviranje.txt", jsonString);
        }

        public List<ProstorijaRenoviranje> ucitajProstorijeZaRenoviranje()
        {
            List<ProstorijaRenoviranje> prostorijeZaRenoviranje;

            try
            {
                prostorijeZaRenoviranje = JsonSerializer.Deserialize<List<ProstorijaRenoviranje>>(File.ReadAllText("Datoteke/ProstorijeRenoviranje.txt"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                prostorijeZaRenoviranje = new List<ProstorijaRenoviranje>();
            }

            return prostorijeZaRenoviranje;
        }

    }
}
