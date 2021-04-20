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
    class TerapijaRepozitorijum
    {

        public List<Terapija> ucitajSve()
        {
            List<Terapija> sveTerapije;
            try
            {
                sveTerapije = JsonSerializer.Deserialize<List<Terapija>>(File.ReadAllText("Datoteke/Terapije.txt"));
            }
            catch (Exception e)
            {
                sveTerapije = new List<Terapija>();
            }

            return sveTerapije;
        }
    
        public void azurirajTerapiju(Terapija terapijaZaAzuriranje)
        {
            List<Terapija> sveTerapije = ucitajSve();
            foreach(Terapija terapija in sveTerapije)
            {
                if (terapija.id.Equals(terapijaZaAzuriranje.id))
                {
                    terapija.id = terapijaZaAzuriranje.id;
                    terapija.idBolesti = terapijaZaAzuriranje.idBolesti;
                    terapija.idLeka = terapijaZaAzuriranje.idLeka;
                    terapija.idPacijenta = terapijaZaAzuriranje.idPacijenta;
                    terapija.idTermina = terapijaZaAzuriranje.idTermina;
                    terapija.nacinUpotrebe = terapijaZaAzuriranje.nacinUpotrebe;
                    terapija.trajanje = terapijaZaAzuriranje.trajanje;
                    terapija.datumPocetka = terapijaZaAzuriranje.datumPocetka; 
                    break;
                }
            }
            upisi(sveTerapije);
        }
        public void upisi(List<Terapija> sveTerapije)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sveTerapije, formatiranje);
            File.WriteAllText("Datoteke/Terapije.txt", jsonString);
        }
    }
}
