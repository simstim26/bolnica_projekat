using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs.Implementacija
{
    class PacijentCRUDImplementacija : ICRUD<Pacijent>
    {
        private PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        public void azuriraj(Pacijent objekat)
        {
            pacijentRepozitorijum.azurirajPacijenta(objekat);
        }

        public void kreiraj(Pacijent objekat)
        {
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            objekat.id = (sviPacijenti.Count() + 1).ToString();


            pacijentRepozitorijum.dodajPacijenta(objekat);
            KorisnikServis.getInstance().dodajKorisnika(objekat.id, objekat.korisnickoIme, objekat.lozinka, "pacijent");
        }

        public void obrisi(string id)
        {
            List<Pacijent> sviPacijenti = ;
            foreach (Pacijent pacijent in sviPacijenti) 
            { 
                if (pacijent.id.Equals(id))
                {
                    pacijent.jeLogickiObrisan = true;

                }
            }
            pacijentRepozitorijum.upisi(sviPacijenti);
        }

        public List<Pacijent> ucitaj()
        {
            List<Pacijent> ucitaniPacijenti = pacijentRepozitorijum.ucitajSve();
            List<Pacijent> neobrisaniPacijenti = new List<Pacijent>();

            foreach (Pacijent p in ucitaniPacijenti)
            {
                if (!p.jeLogickiObrisan)
                {
                    neobrisaniPacijenti.Add(p);
                }
            }
            return neobrisaniPacijenti;
        }
    }
}
