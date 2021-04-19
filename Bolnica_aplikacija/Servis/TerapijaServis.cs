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
        private TerapijaRepozitorijum terapijaRepozitorijum = new TerapijaRepozitorijum();
        public void dodajTerapiju(Terapija terapija)
        {
            List<Terapija> sveTerapije = terapijaRepozitorijum.ucitajSve();
            terapija.id = (sveTerapije.Count + 1).ToString();

            sveTerapije.Add(terapija);
            terapijaRepozitorijum.upisi(sveTerapije);
        }

        public Terapija nadjiTerapijuZaTermin(String idTermina)
        {
            foreach(Terapija t in terapijaRepozitorijum.ucitajSve())
            {
                if (t.idTermina.Equals(idTermina))
                {
                    return t;
                }
            }

            return null;
        }

        public void dodajIdBolestiZaTerapiju(String idTerapije, String idBolesti)
        {
            foreach(Terapija t in terapijaRepozitorijum.ucitajSve())
            {
                if (idTerapije.Equals(t.id))
                {
                    t.idBolesti = idBolesti;
                    terapijaRepozitorijum.azurirajTerapiju(t);
                }
            }
        }
    }
}
