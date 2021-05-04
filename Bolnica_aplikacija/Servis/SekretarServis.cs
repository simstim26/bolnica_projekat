﻿using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bolnica_aplikacija.Servis
{
    class SekretarServis
    {

        private SekretarRepozitorijum sekretarRepozitorijum = new SekretarRepozitorijum();
        private static PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private static KorisnikRepozitorijum korisnikRepozitorijum = new KorisnikRepozitorijum();
        private static AlergijaRepozitorijum alergijaRepozitorijum = new AlergijaRepozitorijum();
        private static TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private static PacijentServis pacijentServis = new PacijentServis();

        public String id { get; set; }
        public String idBolnice { get; set; }

        public void NapraviPacijenta(String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {

            Pacijent pacijent = new Pacijent();
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();

            pacijent.id = (sviPacijenti.Count() + 1).ToString();
            pacijent.idBolnice = idBolnice;
            pacijent.jeGost = gost;
            pacijent.korisnickoIme = korisnickoIme;
            pacijent.lozinka = lozinka;
            pacijent.jmbg = jmbg;
            pacijent.ime = ime;
            pacijent.prezime = prezime;
            pacijent.datumRodjenja = datumRodj;
            pacijent.adresa = adresa;
            pacijent.email = email;
            pacijent.brojTelefona = telefon;

            foreach(Alergija alergija in alergije)
            {               
                alergijaRepozitorijum.dodajAlergiju(new Alergija(pacijent.id, alergija.nazivAlergije));
            }
            
            pacijentRepozitorijum.dodajPacijenta(pacijent);

            //Unos pacijenta u korisnike
            List<PomocnaKlasaKorisnici> korisnici = korisnikRepozitorijum.ucitajSve();
            PomocnaKlasaKorisnici korisnik = new PomocnaKlasaKorisnici();
            korisnik.id = pacijent.id;
            korisnik.korisnickoIme = korisnickoIme;
            korisnik.lozinka = lozinka;
            korisnik.tip = "pacijent";

            korisnikRepozitorijum.dodajKorisnika(korisnik);
          
        }

        public List<Pacijent> ProcitajPacijente()
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

        public void AzurirajPacijenta(String id, String idBolnice, bool gost, String korisnickoIme, String lozinka, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon, List<Alergija> alergije)
        {
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            foreach (Pacijent izmeniP in sviPacijenti)
            {
                if (izmeniP.id.Equals(id))
                {

                    izmeniP.id = id;
                    izmeniP.idBolnice = idBolnice;
                    izmeniP.jeGost = gost;
                    izmeniP.korisnickoIme = korisnickoIme;
                    izmeniP.lozinka = lozinka;
                    izmeniP.jmbg = jmbg;
                    izmeniP.ime = ime;
                    izmeniP.prezime = prezime;
                    izmeniP.datumRodjenja = datumRodj;
                    izmeniP.adresa = adresa;
                    izmeniP.email = email;
                    izmeniP.brojTelefona = telefon;
 
                    alergijaRepozitorijum.azurirajAlergije(alergije, izmeniP.id);                   
                    pacijentRepozitorijum.azurirajPacijenta(izmeniP);
                }
            }
     
        }

        public void ObrisiPacijenta(String idPacijenta)
        {

            //Pronadji pacijenta za brisanje i postavi mu jeLogickiObrisan na true
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            foreach (Pacijent p in sviPacijenti)
            {

                if (p.id.Equals(idPacijenta))
                {
                    p.jeLogickiObrisan = true;

                }
            }

            pacijentRepozitorijum.upisi(sviPacijenti);


        }

    }
}
