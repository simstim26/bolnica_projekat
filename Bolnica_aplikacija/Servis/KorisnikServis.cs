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
    class KorisnikServis
    {
        /*---Za cuvanje ulogovanog korisnika---*/
        private Lekar lekar;
        /*------------------------------------*/
        private KorisnikRepozitorijum korisnikRepozitorijum = new KorisnikRepozitorijum();
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();
        public String[] prijava(String korisnickoIme, String lozinka)
        {
            String[] povratnaVrednost = { "", "" };
            foreach (PomocnaKlasaKorisnici korisnik in korisnikRepozitorijum.ucitajSve())
            {
                if(korisnik.korisnickoIme.Equals(korisnickoIme) && korisnik.lozinka.Equals(lozinka))
                {
                    povratnaVrednost[0] = korisnik.tip;
                    povratnaVrednost[1] = korisnik.id;
                    break;
                }
            }

            return povratnaVrednost;
        }

        public void nadjiLekara(String idLekara) //da li ide u repozitorijum nova metoda nadjiPoId?
        {
            foreach(Lekar lekar in lekarRepozitorijum.ucitajSve())
            {
                if (lekar.id.Equals(idLekara))
                {
                    this.lekar = lekar;
                    break;
                }
            }
        }
        public Lekar getLekar()
        {
            return lekar;
        }
    }
}
