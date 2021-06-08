using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs.Implementacija
{
    class ObavestenjeCRUDImplementacija : ICRUD<Obavestenje>
    {
        private static ObavestenjeRepozitorijum obavestenjeRepozitorijum = new ObavestenjeRepozitorijum();
        public void azuriraj(Obavestenje objekat)
        {
            obavestenjeRepozitorijum.azurirajObavestenje(objekat);
        }

        public void kreiraj(Obavestenje objekat)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();

            objekat.id = (svaObavestenja.Count + 1).ToString();
            obavestenjeRepozitorijum.dodajObavestenje(objekat);
        }

        public void obrisi(string id)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();

            foreach (Obavestenje obavestenje in svaObavestenja)
            {

                if (obavestenje.id.Equals(id))
                {
                    obavestenje.jeLogickiObrisano = true;
                }
            }

            obavestenjeRepozitorijum.upisi(svaObavestenja);
        }

        public List<Obavestenje> ucitaj()
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();
            List<Obavestenje> neobrisanaObavestenja = new List<Obavestenje>();

            foreach (Obavestenje obavestenje in svaObavestenja)
            {
                if (!obavestenje.jeLogickiObrisano)
                {
                    neobrisanaObavestenja.Add(obavestenje);
                }
            }
            return neobrisanaObavestenja;
        }
    }
}
