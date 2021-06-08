using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class ObavestenjeServis
    {
        private static ObavestenjeRepozitorijum obavestenjeRepozitorijum = new ObavestenjeRepozitorijum();
        private static PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();

        public void napraviObavestenje(Obavestenje obavestenje)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();      
            
            obavestenje.id = (svaObavestenja.Count + 1).ToString();
            obavestenjeRepozitorijum.dodajObavestenje(obavestenje);
         
        }

        public List<Obavestenje> ucitajObavestenja()
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();
            List<Obavestenje> neobrisanaObavestenja = new List<Obavestenje>();

            foreach(Obavestenje obavestenje in svaObavestenja)
            {
                if (!obavestenje.jeLogickiObrisano)
                {
                    neobrisanaObavestenja.Add(obavestenje);
                }
            }

            Console.WriteLine(neobrisanaObavestenja.Count + " BR OBAV ZA UCIT");
            return neobrisanaObavestenja;
        }

        public void azurirajObavestenje(Obavestenje obavestenjeIzmena)
        {

            obavestenjeRepozitorijum.azurirajObavestenje(obavestenjeIzmena);

        }

        public void obrisiObavestenje(String idObavestenja)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();
            
            foreach (Obavestenje obavestenje in svaObavestenja)
            {

                if (obavestenje.id.Equals(idObavestenja))
                {
                    obavestenje.jeLogickiObrisano = true;
                }
            }

            obavestenjeRepozitorijum.upisi(svaObavestenja);

        }
    }
}
