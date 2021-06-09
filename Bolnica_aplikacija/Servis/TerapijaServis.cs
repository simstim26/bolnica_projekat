using Bolnica_aplikacija.Repozitorijum; 
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class TerapijaServis
    {
        private static TerapijaServis instance;
        public static TerapijaServis getInstance()
        {
            if(instance == null)
            {
                instance = new TerapijaServis();
            }

            return instance;
        }
        private TerapijaRepozitorijum terapijaRepozitorijum = new TerapijaRepozitorijum();
        public String nadjiNazivLekaZaTerapiju(String idTerapije)
        {
            Terapija terapija = nadjiTerapijuPoId(idTerapije);
            return LekServis.getInstance().nadjiLekPoId(terapija.idLeka).naziv;
        }

        public List<Terapija> ucitajSve()
        {
            return terapijaRepozitorijum.ucitajSve();
        }
        public void azurirajTerapiju(Terapija terapijaZaAzuriranje)
        {
            List<Terapija> sveTerapije = ucitajSve();
            foreach (Terapija terapija in sveTerapije)
            {
                if (terapija.id.Equals(terapijaZaAzuriranje.id))
                {
                    terapija.kopiraj(terapijaZaAzuriranje);
                    break;
                }
            }
            terapijaRepozitorijum.upisi(sveTerapije);
        }

        public Terapija nadjiTerapijuPoId(String idTerapije)
        {
            if (idTerapije == null)
                return new Terapija();

            return terapijaRepozitorijum.ucitajSve().ToDictionary(t => t.id)[idTerapije];
        }

        public String dodajTerapiju(Terapija terapija)
        {
            List<Terapija> sveTerapije = terapijaRepozitorijum.ucitajSve();
            terapija.id = (sveTerapije.Count + 1).ToString();

            sveTerapije.Add(terapija);
            terapijaRepozitorijum.upisi(sveTerapije);
            return terapija.id;
        }
        public Terapija nadjiTerapijuZaTermin(String idTermina)
        {
            foreach(Terapija t in terapijaRepozitorijum.ucitajSve())
            {
                if (t.idTermina != null && t.idTermina.Equals(idTermina))
                {
                    return t;
                }
            }

            return null;
        }

        public void dodajIdBolestiZaTerapiju(String idTerapije, String idBolesti)
        {
            Terapija terapija = nadjiTerapijuPoId(idTerapije);
            terapija.idBolesti = idBolesti;
            azurirajTerapiju(terapija);
        }

        public List<Terapija> ucitajTrenutneTerapijePacijenta(String idPacijenta)
        {
            List<Terapija> pacijentTerapije = new List<Terapija>();

            foreach (Terapija terapija in ucitajSve())
            {
                if(DateTime.Compare(terapija.datumPocetka.AddDays(terapija.trajanje), DateTime.Now) >= 0)
                {
                    pacijentTerapije.Add(terapija);
                }
            }

            return pacijentTerapije;
        }
    }
}
