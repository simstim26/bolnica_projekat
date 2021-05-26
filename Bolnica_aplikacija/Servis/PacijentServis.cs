
﻿using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
﻿using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.Servis
{
    class PacijentServis
    {
        private PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private static PacijentServis instance;

        public static PacijentServis getInstance()
        {
            if(instance == null)
            {
                instance = new PacijentServis();
            }

            return instance;
        }

        public bool proveriAlergijuNaLekZaPacijenta(String idPacijenta, String idIzabraniLek)
        {
            List<String> sastojci = LekServis.getInstance().nadjiLekPoId(idIzabraniLek).sastojci;
            List<Alergija> alergije = AlergijaServis.getInstance().ucitajAlergijeZaPacijenta(idPacijenta);
            if (sastojci == null || alergije == null)
                return false;

            foreach (String sastojak in sastojci)
            {
                foreach(Alergija alergija in alergije)
                {
                    if (sastojak.Equals(alergija.nazivAlergije))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta(String idPacijenta)
        {
            List<BolestTerapija> istorijaBolesti = new List<BolestTerapija>();

            foreach(Bolest bolest in BolestServis.getInstance().ucitajSve())
            {
                if (bolest.pacijent.id.Equals(idPacijenta))
                {
                    Termin termin = TerminServis.getInstance().nadjiTerminZaBolest(bolest.id);
                    Terapija terapija = TerapijaServis.getInstance().nadjiTerapijuPoId(termin.idTerapije);
                    Lek lek = LekServis.getInstance().nadjiLekPoId(terapija.idLeka);

                    istorijaBolesti.Add(new BolestTerapija(bolest.id, bolest.naziv, terapija.id, lek.id, lek.kolicina.ToString(),
                            null, lek.naziv, termin.idTermina, termin.izvestaj, idPacijenta));              
                }
            }

            return istorijaBolesti;
        }

        public List<BolestTerapija> ucitajSveTerapijeZaPacijenta(String idPacijenta)
        {
            List<BolestTerapija> povratnaVrednost = new List<BolestTerapija>();

            foreach (Terapija terapija in TerapijaServis.getInstance().ucitajSve())
            {
                if (terapija.idPacijenta.Equals(idPacijenta))
                {
                    Bolest bolest = BolestServis.getInstance().nadjiBolestPoId(terapija.idBolesti);
                    Lek lek = LekServis.getInstance().nadjiLekPoId(terapija.idLeka);
                    Termin termin = TerminServis.getInstance().nadjiTerminPoId(terapija.idTermina);
                    if (lek != null)
                    {
                        povratnaVrednost.Add(new BolestTerapija(bolest.id, bolest.naziv, terapija.id, lek.id, lek.kolicina.ToString(),
                            proveriAktivnostTerapije(terapija), lek.naziv, termin.idTermina, termin.izvestaj, idPacijenta));
                    }
                }
            }
            return povratnaVrednost;
        }

        private String proveriAktivnostTerapije(Terapija terapija)
        {
            String povratnaVrednost = "Završeno";
            DateTime trenutniDatum = DateTime.Today + new TimeSpan(0, 0, 0);
            DateTime pocetakTerapije = terapija.datumPocetka.Date + new TimeSpan(0, 0, 0);
            DateTime krajTerapije = pocetakTerapije.AddDays(terapija.trajanje);
            if(DateTime.Compare(trenutniDatum, pocetakTerapije) >= 0 && DateTime.Compare(krajTerapije, trenutniDatum) >= 1)
            {
                povratnaVrednost = "U toku";
            }

            return povratnaVrednost;
        }

        public List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            List<Pacijent> pacijentiZaPrikaz = new List<Pacijent>();
            foreach (Pacijent pacijent in sviPacijenti)
            {
                if (!pacijent.jeLogickiObrisan)
                {
                    pacijentiZaPrikaz.Add(pacijent);
                }
            }

            return pacijentiZaPrikaz;

        }

        public void azurirajTerminPacijentu(String idStarogTermina, String idNovogTermina)
        {
            Termin stariTermin = TerminServis.getInstance().nadjiTerminPoId(idStarogTermina);
            Termin noviTermin = TerminServis.getInstance().nadjiTerminPoId(idNovogTermina);

            noviTermin.idPacijenta = stariTermin.idPacijenta;
            TerminServis.getInstance().azurirajTermin(noviTermin);

            stariTermin.idPacijenta = "";
            TerminServis.getInstance().azurirajTermin(stariTermin);
        }

        private void radSaPacijentTerminomPrikazPacijentovihTermina(Termin termin, PacijentTermin pacijentTermin)
        {
            popuniProstoriju(termin, pacijentTermin);
            popuniLekara(termin, pacijentTermin);
            dopuniPacijentTermin(termin, pacijentTermin);

        }

        private void popuniLekara(Termin termin, PacijentTermin pacijentTermin)
        {
            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                pacijentTermin.imeLekara = "";

                if (lekar.id.Equals(termin.idLekara))
                {
                    /*if (lekar.idSpecijalizacije.Equals("0") || jeSekretar && lekar.idSpecijalizacije.Equals("0"))
                     {
                         pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                         pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                         break;
                     }
                    else*/

                    pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                    pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                    pacijentTermin.nazivSpecijalizacije = SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(lekar.idSpecijalizacije);
                    break;

                }
            }
        }

        public List<PacijentTermin> prikazPacijentovihTermina(String idPacijenta)
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if(termin.idPacijenta.Equals(idPacijenta))
                {

                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);

                    if (rezultat > 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();

                        radSaPacijentTerminomPrikazPacijentovihTermina(termin, pacijentTermin);
                        
                        terminiPacijenta.Add(pacijentTermin);
                    }
                }
            }

            return terminiPacijenta;
        }

        public List<PacijentTermin> prikazBuducihTerminaPacijenta(String idPacijenta)
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(idPacijenta) && DateTime.Compare(termin.datum, DateTime.Today) >= 0)
                {
                    terminiPacijenta.Add(new PacijentTermin(termin.idTermina, termin.datum.Date.ToString("dd.MM.yyyy."), termin.satnica.ToString("HH:mm"), ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije),
                        termin.getTipString(), termin.idLekara, LekarServis.getInstance().pronadjiImeLekara(termin.idLekara), LekarServis.getInstance().pronadjiNazivSpecijalizacijeLekara(termin.idLekara),
                        LekarServis.getInstance().nadjiLekaraPoId(termin.idLekara).idSpecijalizacije, null));
                 
                }
            }

            return terminiPacijenta;
        }

        public List<PacijentTermin> prikazProslihTerminaPacijenta(String idPacijenta)
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(idPacijenta) && termin.jeZavrsen)
                {                   
                    terminiPacijenta.Add(new PacijentTermin(termin.idTermina, termin.datum.Date.ToString("dd.MM.yyyy."), termin.satnica.ToString("HH:mm"), ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije),
                        termin.getTipString(), termin.idLekara, LekarServis.getInstance().pronadjiImeLekara(termin.idLekara), LekarServis.getInstance().pronadjiNazivSpecijalizacijeLekara(termin.idLekara),
                        LekarServis.getInstance().nadjiLekaraPoId(termin.idLekara).idSpecijalizacije, TerapijaServis.getInstance().nadjiNazivLekaZaTerapiju(termin.idTerapije)));  
                    
                }
            }

            return terminiPacijenta;
        }

        public void zakaziTerminPacijentu(String idPacijenta, String idTermina)
        {
            Termin termin = TerminServis.getInstance().nadjiTerminPoId(idTermina);
            termin.idPacijenta = idPacijenta;
            TerminServis.getInstance().azurirajTermin(termin);
        }
        public void otkaziTerminPacijenta(String idTermina)
        {
            Termin termin = TerminServis.getInstance().nadjiTerminPoId(idTermina);
            termin.idPacijenta = "";
            TerminServis.getInstance().azurirajTermin(termin);
        }
        public Pacijent nadjiPacijenta(String idPacijenta)
        {
            //OBAVEZNO IZMENITI!!!!!
            var sviPacijenti = pacijentRepozitorijum.ucitajSve();

            foreach(Pacijent pacijent in sviPacijenti)
            {
                if (pacijent.id.Equals(idPacijenta))
                {
                    return pacijent;

                    /*this.pacijent = pacijent;
                    break;*/
                }
            }
            
            return new Pacijent();
        }


        public List<PacijentTermin> ucitajSlobodneTermine(int indikator, bool jeSekretar)
        { 
            if (indikator == 0)
            {
                return prikazSlobodnihTerminaZakazivanje(jeSekretar);
            }
            else
            {
                return prikazSlobodnihTerminaPomeranje(jeSekretar);
            }
        }

        private List<PacijentTermin> prikazSlobodnihTerminaZakazivanje(bool jeSekretar)
        {
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {           
                bool terminJeTokomGodisnjegOdmora = TerminServis.getInstance().terminJeTokomGodisnjegOdmoraLekara(termin);
                if (termin.idPacijenta.Equals("") && !terminJeTokomGodisnjegOdmora)
                {
                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);
                    if (rezultat > 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();

                        popuniPacijentTermin(termin, pacijentTermin, jeSekretar);

                        if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            dopuniPacijentTermin(termin, pacijentTermin);
                            terminiSlobodni.Add(pacijentTermin);
                        }
                    }
                }
            }
            return terminiSlobodni;
        }

        private List<PacijentTermin> prikazSlobodnihTerminaPomeranje(bool jeSekretar)
        {
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();
            

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {
                    
                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);
                    int rezultatGornji = DateTime.Compare(termin.datum, DateTime.Today.AddDays(3));
                    int rezultatDodatni = DateTime.Compare(DateTime.Today.AddDays(1), termin.datum);

                    //rezultat je za poredjenje danasnjeg datuma i terminovog datuma, 
                    //rezultatGornji proverava da li je datum unutar ogranicenja od 3 dana za pomeranje
                    //rezultatDodatni proverava da li je termin dan posle danasnjeg jer ukoliko jeste ne bi trebalo da sme da se pomeri na taj datum

                    if (rezultat > 0 && rezultatGornji < 0 && rezultatDodatni < 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();

                        popuniPacijentTermin(termin, pacijentTermin, jeSekretar);

                        if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            dopuniPacijentTermin(termin, pacijentTermin);
                            terminiSlobodni.Add(pacijentTermin);
                        }
                    }
                }
            }
            return terminiSlobodni;
        }

        public void popuniPacijentTermin(Termin termin, PacijentTermin pacijentTermin, bool jeSekretar)
        {
            popuniProstoriju(termin, pacijentTermin);
            popuniLekara(termin, pacijentTermin);
            
        }

        private void popuniProstoriju(Termin termin, PacijentTermin pacijentTermin)
        {
            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
            {
                pacijentTermin.lokacija = "";

                if (prostorija.id.Equals(termin.idProstorije))
                {
                    pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                    break;
                }
            }
        }

        public void dopuniPacijentTermin(Termin termin, PacijentTermin pacijentTermin)
        {
            pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
            pacijentTermin.napomena = termin.getTipString();
            pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
            pacijentTermin.id = termin.idTermina;
        }

        public List<PacijentTermin> filtrirajTermine(int indikator, String kriterijum)
        {
            //ako je indikator 0 onda se pretrazuje tj filtrira tabela u odnosu na lekare
            //ako je indikator 1 onda se radi to u odnosu na vreme odrzavanja 

            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();

            if (indikator == 0)
            {
                return filtrirajPoLekaru(kriterijum);
            }
            else if (indikator == 1)
            {
                return filtrirajPoDatumima(kriterijum);
            }
            else
                return null;
            
        }

        public List<PacijentTermin> filtrirajPoDatumima(String kriterijum)
        {
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();
            try
            {
                radSaPodacimaFiltriranjaPoDatumima(terminiSlobodni, kriterijum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return terminiSlobodni;

        }

        public void radSaPodacimaFiltriranjaPoDatumima(List<PacijentTermin> terminiSlobodni, String kriterijum)
        {
            DateTime kriterijumPretrage = new DateTime();
            kriterijumPretrage = Convert.ToDateTime(kriterijum);

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {
                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);
                    int rezultatPretrage = DateTime.Compare(termin.datum, kriterijumPretrage);

                    if (rezultat > 0 && rezultatPretrage == 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        popuniPacijentTermin(termin, pacijentTermin, false);

                        if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            dopuniPacijentTermin(termin, pacijentTermin);
                            terminiSlobodni.Add(pacijentTermin);
                        }
                    }
                }
            }
        }

        public List<PacijentTermin> filtrirajPoLekaru(String kriterijum)
        {
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();

            String[] podaci = kriterijum.Split(' ');

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {

                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);

                    if (rezultat > 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();

                        filtriranjePoLekaruPostavljanjePolja(termin, pacijentTermin, podaci);

                        if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            filtriranjePoLekaruDopunjavanjePolja(termin, pacijentTermin);
                            terminiSlobodni.Add(pacijentTermin);
                        }
                    }
                }
            }
            return terminiSlobodni;
        }

        private void filtriranjePoLekaruPostavljanjePolja(Termin termin, PacijentTermin pacijentTermin, String[] podaci)
        {
            popuniProstoriju(termin, pacijentTermin);
            filtriranjePoLekaruPostavljanjeLekara(termin, pacijentTermin, podaci);
        }

        private void filtriranjePoLekaruPostavljanjeLekara(Termin termin, PacijentTermin pacijentTermin, String []podaci)
        {
            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                pacijentTermin.imeLekara = "";

                if (podaci.Length == 2)
                {
                    if (lekar.id.Equals(termin.idLekara) && lekar.ime.ToLower().Contains(podaci[0].ToLower()) && lekar.prezime.ToLower().Contains(podaci[1].ToLower()))
                    {
                        if (lekar.idSpecijalizacije.Equals("0"))
                        {
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                            break;
                        }
                    }
                }
                if (podaci.Length == 1)
                {
                    if (lekar.id.Equals(termin.idLekara) && lekar.ime.ToLower().Contains(podaci[0].ToLower()))
                    {
                        if (lekar.idSpecijalizacije.Equals("0"))
                        {
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                            break;
                        }
                    }
                }
            }
        }
        private void filtriranjePoLekaruDopunjavanjePolja(Termin termin, PacijentTermin pacijentTermin)
        {
            pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
            pacijentTermin.napomena = termin.tip.ToString();
            pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
            pacijentTermin.id = termin.idTermina;
        }

        public void napraviAlergiju(String idPacijenta, String nazivAlergije)
        {
            Alergija alergija = new Alergija(idPacijenta, nazivAlergije);
            AlergijaServis.getInstance().dodajAlergiju(alergija);
        }

        public List<Alergija> procitajAlergije(String idPacijenta)
        {
            List<Alergija> alergije = new List<Alergija>();

            foreach (Alergija alergija in AlergijaServis.getInstance().ucitajSve())
            {
                if (alergija.idPacijenta.Equals(idPacijenta))
                {
                    alergije.Add(alergija);
                }

            }

            return alergije;
        }

        public bool proveriStanjeAnkete(String idPacijenta)
        {
            foreach(Pacijent pacijent in pacijentRepozitorijum.ucitajSve())
            {
                if(pacijent.id.Equals(idPacijenta))
                {
                    return pacijent.anketa;
                }
            }

            return false;
        }

        public void postaviStanjeAnkete(String idPacijenta)
        {
            List<Pacijent> sviPacijenti = new List<Pacijent>();

            foreach (Pacijent pacijent in pacijentRepozitorijum.ucitajSve())
            {
                if (pacijent.id.Equals(idPacijenta))
                {
                    pacijent.anketa = !pacijent.anketa;
                }

                sviPacijenti.Add(pacijent);

            }

            pacijentRepozitorijum.upisi(sviPacijenti);

        }

        public List<PacijentTermin> ucitajZauzeteTermine()

        {
            List<PacijentTermin> terminiZauzeti = new List<PacijentTermin>();

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (!termin.idPacijenta.Equals("") && !termin.jeHitan)
                {
                    if (terminJeUNarednihSatVremena(termin))
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        PacijentServis.getInstance().popuniPacijentTermin(termin, pacijentTermin, true);

                        if (!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            PacijentServis.getInstance().dopuniPacijentTermin(termin, pacijentTermin);
                            terminiZauzeti.Add(pacijentTermin);
                        }

                    }
                }
            }
            return terminiZauzeti;
        }

        private bool terminJeUNarednihSatVremena(Termin termin)
        {
            DateTime terminDatum = termin.datum;
            DateTime datumSatUnapred = DateTime.Now.AddHours(1);
            bool jeUSatVremena = false;

            int rezultat1 = DateTime.Compare(terminDatum, datumSatUnapred);
            int rezultat2 = DateTime.Compare(terminDatum, DateTime.Now);
            
            if (rezultat1 <= 0 && rezultat2 > 0)
            {
                jeUSatVremena = true;
            }
            
            return jeUSatVremena;
        }

        public void pomeriTerminNaPrviSlobodan(String idPacijenta, String idTermina, String tip, String idSpecijalizacije)
        {
            bool pronadjenTermin = false;
            foreach (PacijentTermin pacijentTermin in ucitajSlobodneTermine(0, true))
            {
                if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                {                   
                    PacijentKontroler.azurirajTerminPacijentu(idTermina, pacijentTermin.id);
                    pronadjenTermin = true;
                    NotifikacijaKontroler.napraviNotifikaciju("Pomeranje termina (Pacijent)", "Pomeren je termin (Pacijent)", idPacijenta, "pacijent");
                    NotifikacijaKontroler.napraviNotifikaciju("Pomeranje termina (Lekar)", "Pomeren je termin (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(pacijentTermin.id), "lekar");
                    break;
                }
            }

            if (!pronadjenTermin)
            {
                NotifikacijaKontroler.napraviNotifikaciju("Otkazivanje termina (Pacijent)", "Otkazan je termin usled pomeranja (Pacijent)", idPacijenta, "pacijent");

            }

        }

        public void NapraviPacijenta(Pacijent pacijent, List<Alergija> alergije)
        {
            
            List<Pacijent> sviPacijenti = pacijentRepozitorijum.ucitajSve();
            pacijent.id = (sviPacijenti.Count() + 1).ToString();
            
            foreach (Alergija alergija in alergije)
            {
                AlergijaServis.getInstance().dodajAlergiju(new Alergija(pacijent.id, alergija.nazivAlergije));
            }

            pacijentRepozitorijum.dodajPacijenta(pacijent);
            KorisnikServis.getInstance().dodajKorisnika(pacijent.id, pacijent.korisnickoIme, pacijent.lozinka, "pacijent");
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

        public void AzurirajPacijenta(Pacijent izmeniPacijent, List<Alergija> alergije)
        {
            AlergijaServis.getInstance().azurirajAlergije(alergije, izmeniPacijent.id);
            pacijentRepozitorijum.azurirajPacijenta(izmeniPacijent);

        }

        public void ObrisiPacijenta(String idPacijenta)
        {
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

        public List<TerapijaPacijent> ucitajAktivneTerapije(String idPacijenta)
        {
            List<Terapija> trenutneTerapijePacijenta = TerapijaServis.getInstance().ucitajTrenutneTerapijePacijenta(idPacijenta);
            List<TerapijaPacijent> aktivneTerapije = new List<TerapijaPacijent>();

            foreach(Terapija terapija in trenutneTerapijePacijenta)
            {
                String bolest = BolestKontroler.pronadjiNazivBolestiPoId(terapija.idBolesti);
                String lek = LekKontroler.pronadjiImeLekaPoId(terapija.idLeka);
                TerapijaPacijent terapijaPacijent = new TerapijaPacijent(bolest, lek, terapija.nacinUpotrebe, terapija.id);

                terapijaPacijent.nazivLeka = lek;
                terapijaPacijent.nazivOboljenja = bolest;
                terapijaPacijent.opisTerapije = terapija.nacinUpotrebe;
                terapijaPacijent.idTerapije = terapija.id;

                aktivneTerapije.Add(terapijaPacijent);

                //Console.WriteLine(bolest);

            }

            return aktivneTerapije;

        }

    }
}
