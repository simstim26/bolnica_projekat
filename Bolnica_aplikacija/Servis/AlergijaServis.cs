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
            if(instance == null)
            {
                instance = new AlergijaServis();
            }

            return instance;
        }

        public List<Alergija> ucitajAlergijeZaPacijenta(String idPacijenta)
        {
            if (idPacijenta == null)
                return new List<Alergija>();

            return (ucitajSve().GroupBy(a => a.idPacijenta).ToDictionary(a1 => a1.Key, a1 => a1.ToList()))[idPacijenta];
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
            foreach(Alergija a in alergije)
            {
                if(alergija.idPacijenta.Equals(a.idPacijenta) && alergija.nazivAlergije.Equals(a.nazivAlergije))
                {
                    alergije.Remove(a);
                    break;
                }
            }
            alergijaRepozitorijum.upisi(alergije);
        }

        public void azurirajAlergije(List<Alergija> alergije, String id)
        {
            alergijaRepozitorijum.azurirajAlergije(alergije, id);
        }
    }
}
