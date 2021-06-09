using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class AlergijaServis
    {
        private static AlergijaServis instance;
        private static AlergijaRepozitorijum alergijaRepozitorijum = new AlergijaRepozitorijum();


        public static AlergijaServis getInstance()
        {
            if (instance == null)
            {
                instance = new AlergijaServis();
            }
            return instance;
        }

        public List<Alergija> ucitajAlergijeZaPacijenta(String idPacijenta)
        {
            List<Alergija> povratnaVrednost;
            try
            {
                povratnaVrednost = (ucitajSve().GroupBy(a => a.idPacijenta).ToDictionary(a1 => a1.Key, a1 => a1.ToList()))[idPacijenta];
            }
            catch (Exception e)
            {
                povratnaVrednost = new List<Alergija>();
            }           
            return povratnaVrednost;
        }

        public List<Alergija> ucitajSve()
        {
            return alergijaRepozitorijum.ucitajSve();
        }

        public void dodajAlergiju(Alergija alergija)
        {
            alergijaRepozitorijum.dodajAlergiju(alergija);
        }

        public void obrisiAlergiju(Alergija alergija)
        {
            List<Alergija> alergije = alergijaRepozitorijum.ucitajSve();
            foreach (Alergija a in alergije)
            {
                if (alergija.idPacijenta.Equals(a.idPacijenta) && alergija.nazivAlergije.Equals(a.nazivAlergije))
                {
                    alergije.Remove(a);
                    break;
                }
            }
            alergijaRepozitorijum.upisi(alergije);
        }

        public void azurirajAlergije(List<Alergija> alergije, String id)
        {
            List<Alergija> sveAlergije = ucitajOstaleAlergije(id);

            foreach (Alergija alergijaIzmena in alergije)
            {
                if (!alergijaIzmena.nazivAlergije.Equals(""))
                {
                    sveAlergije.Add(alergijaIzmena);
                }

            }           
            alergijaRepozitorijum.azurirajAlergije(sveAlergije);
        }

        private List<Alergija> ucitajOstaleAlergije(String idPacijenta)
        {
            List<Alergija> povratna = new List<Alergija>();
            foreach (Alergija alergija in ucitajSve())
            {
                if (!alergija.idPacijenta.Equals(idPacijenta))
                {
                    povratna.Add(alergija);
                }
            }
            return povratna;
        }


        public void dodajAlergijePacijentu(String idPacijenta, List<Alergija> alergije)
        {
            foreach (Alergija alergija in alergije)
            {
                AlergijaServis.getInstance().dodajAlergiju(new Alergija(idPacijenta, alergija.nazivAlergije));
            }
		}
        public bool proveriPostojanjeAlergije(String idPacijenta, String nazivAlergije)
        {
            List<Alergija> alergije = PacijentServis.getInstance().procitajAlergije(idPacijenta);
            bool povratnaVrednost = false;

            foreach(Alergija alergija in alergije)
            {
                if(alergija.nazivAlergije.Trim().Equals(nazivAlergije))
                {
                    povratnaVrednost = true;
                    break;
                }
            }

            return povratnaVrednost;
        }
    }
}
