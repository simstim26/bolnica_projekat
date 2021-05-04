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
        private static KorisnikServis instance;
        public static KorisnikServis getInstance()
        {
            if(instance == null)
            {
                instance = new KorisnikServis();
            }

            return instance;
        }

        /*---Za cuvanje ulogovanog korisnika---*/
        private Lekar lekar;
        private Pacijent pacijent;
        private Upravnik upravnik;
        private Sekretar sekretar;
        /*-------------------------------------*/

        private KorisnikRepozitorijum korisnikRepozitorijum = new KorisnikRepozitorijum();
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();
        private PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private UpravnikRepozitorijum upravnikRepozitorijum = new UpravnikRepozitorijum();
        private SekretarRepozitorijum sekretarRepozitorijum = new SekretarRepozitorijum();

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

        public void NadjiPacijenta(String idPacijenta)
        {
            foreach(Pacijent pacijent in pacijentRepozitorijum.ucitajSve())
            {
                if(pacijent.id.Equals(idPacijenta))
                {
                    this.pacijent = pacijent;
                    break;
                }
            }
        }

        public void NadjiUpravnika(String idUpravnika)
        {
            foreach(Upravnik upravnik in upravnikRepozitorijum.UcitajSve())
            {
                if(upravnik.id == idUpravnika)
                {
                    this.upravnik = upravnik;
                    break;
                }
            }
        }

        public void nadjiSekretara(String idSekretara)
        {
            foreach (Sekretar sekretar in sekretarRepozitorijum.ucitajSve())
            {
                if (sekretar.id == idSekretara)
                {
                    this.sekretar = sekretar;
                    break;
                }
            }
        }

        public Pacijent GetPacijent()
        {
            return pacijent;
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

        public Upravnik GetUpravnik()
        {
            return upravnik;
        }

        public Sekretar getSekretar()
        {
            return sekretar;
        }

        public void dodajKorisnika(String id, String korisnickoIme, String lozinka, String tipKorisnika)
        {
            PomocnaKlasaKorisnici korisnik = new PomocnaKlasaKorisnici();
            korisnik.id = id;
            korisnik.korisnickoIme = korisnickoIme;
            korisnik.lozinka = lozinka;
            korisnik.tip = tipKorisnika;

            korisnikRepozitorijum.dodajKorisnika(korisnik);
        }

    }
}
