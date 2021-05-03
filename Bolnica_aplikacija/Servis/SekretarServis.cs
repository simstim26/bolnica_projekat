using Bolnica_aplikacija.Kontroler;
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

        public List<PacijentTermin> ucitajTermineZaHitanSlucaj(String tip, String idSpecijalizacije)
        {
            List<PacijentTermin> sviTermini = pacijentServis.ucitajSlobodneTermine(0, true);
            List<PacijentTermin> datumiUOpsegu = new List<PacijentTermin>();
            List<PacijentTermin> filtriraniTermini = new List<PacijentTermin>();

            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                foreach(PacijentTermin pacijentTermin in sviTermini)
                {
                    if (pacijentTermin.id.Equals(termin.idTermina))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime trenutniDatum = DateTime.Now.AddHours(1);

                        int rezultat1 = DateTime.Compare(terminDatum, trenutniDatum);
                        int rezultat2 = DateTime.Compare(terminDatum, DateTime.Now);
                        
                        if (rezultat1 <= 0 && rezultat2 > 0)
                        {
                            datumiUOpsegu.Add(pacijentTermin);
                        }
                      
                    }
                }
            }

            foreach (PacijentTermin pacijentTermin in datumiUOpsegu)
            {
                if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                {
                    filtriraniTermini.Add(pacijentTermin);
                }
            }

            if(filtriraniTermini.Count == 0)
            {
                sviTermini = ucitajZauzeteTermine();
                
                foreach (PacijentTermin pacijentTermin in sviTermini)
                {
                    if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                    {
                        filtriraniTermini.Add(pacijentTermin);
                    }
                }
            }
          
                
            return filtriraniTermini;
        }

        public List<PacijentTermin> ucitajZauzeteTermine()

        {
            List<PacijentTermin> terminiZauzeti = new List<PacijentTermin>();

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
                {
                    if (!termin.idPacijenta.Equals(""))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime trenutniDatum = DateTime.Now.AddHours(1);

                        int rezultat1 = DateTime.Compare(terminDatum, trenutniDatum);
                        int rezultat2 = DateTime.Compare(terminDatum, DateTime.Now);

                    if (rezultat1 <= 0 && rezultat2 > 0)
                        {
                            PacijentTermin pacijentTermin = new PacijentTermin();
                            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
                            {
                                if (prostorija.id.Equals(termin.idProstorije))
                                {
                                    pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                                    break;
                                }
                                else
                                {
                                    pacijentTermin.lokacija = "";
                                }
                            }

                            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
                            {
                                if (lekar.id.Equals(termin.idLekara))
                                {
                                    if (lekar.idSpecijalizacije.Equals("0"))
                                    {
                                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                        pacijentTermin.idSpecijalizacije = "0";
                                        Console.WriteLine(pacijentTermin.imeLekara + " " + pacijentTermin.idSpecijalizacije);
                                        break;
                                    }
                                    else if (lekar.idSpecijalizacije.Equals("1"))
                                    {
                                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                        pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                                        Console.WriteLine(pacijentTermin.imeLekara + " " + pacijentTermin.idSpecijalizacije);
                                        break;
                                    }
                                    else
                                    {
                                        pacijentTermin.imeLekara = "";
                                    }
                                }
                                else
                                {
                                    pacijentTermin.imeLekara = "";
                                }
                            }

                            if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                            {
                                pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
                                switch (termin.tip)
                                {
                                    case TipTermina.OPERACIJA: pacijentTermin.napomena = "Operacija"; break;
                                    case TipTermina.PREGLED: pacijentTermin.napomena = "Pregled"; break;
                                    default: break;
                                }
                                pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                                pacijentTermin.id = termin.idTermina;

                                terminiZauzeti.Add(pacijentTermin);
                            }

                        }
                    }
                }        
            return terminiZauzeti;
        }

        public void pomeriTerminNaPrviSlobodan(String idPacijenta, String idTermina, String tip, String idSpecijalizacije)
        {
            List<PacijentTermin> sviTermini = pacijentServis.ucitajSlobodneTermine(0, true);
            bool pronadjenTermin = false;

            foreach (PacijentTermin pacijentTermin in sviTermini)
            {
                if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                {
                    PacijentKontroler.nadjiPacijenta(idPacijenta);
                    PacijentKontroler.azurirajTerminPacijentu(idTermina, pacijentTermin.id);
                    pronadjenTermin = true;

                    NotifikacijaKontroler.napraviNotifikaciju("Pomeranje termina (Pacijent)", "Pomeren je termin (Pacijent)", idPacijenta, "pacijent");
                    NotifikacijaKontroler.napraviNotifikaciju("Pomeranje termina (Lekar)", "Pomeren je termin (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(pacijentTermin.id), "lekar");


                    break;
                }
            }

            if (!pronadjenTermin)
            {
                Console.WriteLine("Nema slobodan termin");
            }        

        }


    }
}
