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

        public void napraviObavestenje(String naslovObavestenja, String sadrzajObavestenja)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();      
            napraviNotifikacije(naslovObavestenja);
            obavestenjeRepozitorijum.dodajObavestenje(new Obavestenje((svaObavestenja.Count + 1).ToString(), naslovObavestenja, sadrzajObavestenja, null));
         
        }
        public void napraviNotifikacije(String porukaNotifikacije)
        {
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            
            foreach(Pacijent pacijent in sviPacijenti)
            {
                NotifikacijaKontroler.napraviNotifikaciju("Nova vest", porukaNotifikacije, pacijent.id, "pacijent");
            }
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

            return neobrisanaObavestenja;
        }

        public void azurirajObavestenje(String id, String naslovObavestenja, String sadrzajObavestenja)
        {
            List<Obavestenje> svaObavestenja = obavestenjeRepozitorijum.ucitajSve();

            foreach(Obavestenje obavestenje in svaObavestenja)
            {
                if (obavestenje.id.Equals(id))
                {
                    obavestenje.naslovObavestenja = naslovObavestenja;
                    obavestenje.sadrzajObavestenja = sadrzajObavestenja;

                    obavestenjeRepozitorijum.azurirajObavestenje(obavestenje);
                }
            }
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
