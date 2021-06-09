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
    class LekarCRUDImplementacija : ICRUD<Lekar>
    {
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();
        public void azuriraj(Lekar objekat)
        {
            lekarRepozitorijum.azurirajLekara(objekat);
        }

        public void kreiraj(Lekar objekat)
        {
            List<Lekar> sviLekari = lekarRepozitorijum.ucitajSve();
            objekat.id = (sviLekari.Count() + 1).ToString();

            lekarRepozitorijum.dodajLekara(objekat);
            KorisnikServis.getInstance().dodajKorisnika(objekat.id, objekat.korisnickoIme, objekat.lozinka, "lekar");
        }

        public void obrisi(string id)
        {
            List<Lekar> sviLekari = lekarRepozitorijum.ucitajSve();
            foreach (Lekar lekar in sviLekari)
            {
                if (lekar.id.Equals(id))
                {
                    lekar.jeLogickiObrisan = true;
                }
            }

            lekarRepozitorijum.upisi(sviLekari);
        }

        public List<Lekar> ucitaj()
        {
            List<Lekar> ucitaniLekari = lekarRepozitorijum.ucitajSve();
            List<Lekar> neobrisaniLekari = new List<Lekar>();

            foreach (Lekar lekar in ucitaniLekari)
            {
                if (!lekar.jeLogickiObrisan)
                {
                    neobrisaniLekari.Add(lekar);
                }
            }
            return neobrisaniLekari;
        }
    }
}
