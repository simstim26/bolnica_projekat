
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
        private Pacijent pacijent; //lekar -> cuva se izabrani pacijent
        private BolestTerapija bolestTerapija;
        private static PacijentServis instance;

        public static PacijentServis getInstance()
        {
            if(instance == null)
            {
                instance = new PacijentServis();
            }

            return instance;
        }


        public List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta()
        {
            List<BolestTerapija> istorijaBolesti = new List<BolestTerapija>();

            foreach(Bolest bolest in BolestServis.getInstance().ucitajSve())
            {
                if (bolest.idPacijenta.Equals(pacijent.id))
                {
                    if (bolest.idTerapije == null)
                    {
                        Termin termin = TerminServis.getInstance().nadjiTerminZaBolest(bolest.id);

                        BolestTerapija bolestTerapija = new BolestTerapija();
                        bolestTerapija.idBolesti = bolest.id;
                        bolestTerapija.nazivBolesti = bolest.naziv;
                        bolestTerapija.idTermina = termin.idTermina;
                        bolestTerapija.izvestaj = termin.izvestaj;
                        istorijaBolesti.Add(bolestTerapija);
                    }
                    else
                    {
                        foreach (Terapija terapija in TerapijaServis.getInstance().ucitajSve())
                        {
                            if (terapija.idTermina != null && terapija.idBolesti.Equals(bolest.id))
                            {
                                Termin termin = TerminServis.getInstance().nadjiTerminZaBolest(terapija.idBolesti);
                                Lek lek = LekServis.getInstance().nadjiLekPoId(terapija.idLeka);
                                if (lek != null)
                                {
                                    BolestTerapija bolestTerapija = new BolestTerapija();
                                    bolestTerapija.idBolesti = bolest.id;
                                    bolestTerapija.nazivBolesti = bolest.naziv;
                                    bolestTerapija.idTerapije = terapija.id; 
                                    bolestTerapija.nazivTerapije = lek.naziv;
                                    bolestTerapija.idTermina = termin.idTermina;
                                    bolestTerapija.izvestaj = termin.izvestaj;
                                    bolestTerapija.idLeka = lek.id;
                                    bolestTerapija.kolicina = lek.kolicina.ToString();
                                    istorijaBolesti.Add(bolestTerapija);
                                }
                                break;
                            }

                        }
                    }
                }
            }

            return istorijaBolesti;
        }

        public List<BolestTerapija> ucitajSveTerapijeZaPacijenta()
        {
            List<BolestTerapija> povratnaVrednost = new List<BolestTerapija>();

            foreach (Terapija terapija in TerapijaServis.getInstance().ucitajSve())
            {
                if (terapija.idPacijenta.Equals(pacijent.id))
                {
                    Bolest bolest = BolestServis.getInstance().nadjiBolestPoId(terapija.idBolesti);
                    Lek lek = LekServis.getInstance().nadjiLekPoId(terapija.idLeka);
                    if (lek != null)
                    {
                        BolestTerapija bolestTerapija = new BolestTerapija();
                        bolestTerapija.idBolesti = bolest.id;
                        bolestTerapija.nazivBolesti = bolest.naziv;
                        bolestTerapija.idTerapije = terapija.id;
                        bolestTerapija.nazivTerapije = lek.naziv;
                        bolestTerapija.idLeka = lek.id;
                        bolestTerapija.kolicina = lek.kolicina.ToString();
                        povratnaVrednost.Add(bolestTerapija);
                    }
                }
            }
            return povratnaVrednost;
        }

        public void sacuvajBolestTerapiju(BolestTerapija bolestTerapija)
        {
            this.bolestTerapija = bolestTerapija;
        }

        public BolestTerapija getBolestTerapija()
        {
            return bolestTerapija;
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
            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (idStarogTermina.Equals(termin.idTermina)) //otkazivanje starog termina
                {
                    termin.idPacijenta = "";
                    TerminServis.getInstance().azurirajTermin(termin);
                }

                if (idNovogTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = pacijent.id;
                    TerminServis.getInstance().azurirajTermin(termin); 
                }
            }

        }

        private void radSaPacijentTerminomPrikazPacijentovihTermina(Termin termin, PacijentTermin pacijentTermin)
        {
            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
            {
                if (prostorija.id.Equals(termin.idProstorije))
                {
                    pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                    break;
                }
            }

            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                if (lekar.id.Equals(termin.idLekara))
                {
                    pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                    pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                    break;
                }
            }

            pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
            switch (termin.tip)
            {
                case TipTermina.OPERACIJA: pacijentTermin.napomena = "Operacija"; break;
                case TipTermina.PREGLED: pacijentTermin.napomena = "Pregled"; break;
                default: break;
            }
            pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
            pacijentTermin.id = termin.idTermina;
        }

        public List<PacijentTermin> prikazPacijentovihTermina()
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if(termin.idPacijenta.Equals(pacijent.id))
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

        public List<PacijentTermin> prikazBuducihTerminaPacijenta()
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(pacijent.id))
                {
                    DateTime terminDatum = termin.datum;
                    DateTime danasnjiDatum = DateTime.Today;

                    int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                    if (rezultat >= 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.napomena = termin.getTipString();
                        pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.imeLekara = LekarServis.getInstance().pronadjiImeLekara(termin.idLekara);
                        pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);
                        terminiPacijenta.Add(pacijentTermin);
                    }
                }
            }

            return terminiPacijenta;
        }

        public List<PacijentTermin> prikazProslihTerminaPacijenta()
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(pacijent.id))
                {
                    if (termin.jeZavrsen)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.napomena = termin.getTipString();
                        pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.imeLekara = LekarServis.getInstance().pronadjiImeLekara(termin.idLekara);
                        pacijentTermin.nazivSpecijalizacije = LekarServis.getInstance().pronadjiNazivSpecijalizacijeLekara(termin.idLekara);
                        pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);
                        pacijentTermin.nazivTerapije = TerapijaServis.getInstance().nadjiNazivLekaZaTerapiju(termin.idTerapije);

                        terminiPacijenta.Add(pacijentTermin);
                    }
                    
                }
            }

            return terminiPacijenta;
        }

        public void zakaziTerminPacijentu(String idTermina)
        {
            foreach(Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = pacijent.id;
                    TerminServis.getInstance().azurirajTermin(termin);
                    break;
                }
            }
        }
        public void otkaziTerminPacijenta(String idTermina)
        {
            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                    TerminServis.getInstance().azurirajTermin(termin);
                    break;
                }
            }
        }
        public void nadjiPacijenta(String idPacijenta)
        {
            var sviPacijenti = pacijentRepozitorijum.ucitajSve();

            foreach(Pacijent pacijent in sviPacijenti)
            {
                if (pacijent.id.Equals(idPacijenta))
                {
                    this.pacijent = pacijent;
                    break;
                }
            }
        }

        public Pacijent getPacijent()
        {
            return pacijent;
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
                if (termin.idPacijenta.Equals(""))
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
            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
            {
                pacijentTermin.lokacija = "";

                if (prostorija.id.Equals(termin.idProstorije))
                {
                    pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                    break;
                }
            }

            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                pacijentTermin.imeLekara = "";

                if (lekar.id.Equals(termin.idLekara))
                {
                    if (lekar.idSpecijalizacije.Equals("0") || jeSekretar && lekar.idSpecijalizacije.Equals("0") )
                    {
                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
						pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                        break;
                    }
					else 
					{
						pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
						pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                        break;
					}
                }
            }
        }

        public void dopuniPacijentTermin(Termin termin, PacijentTermin pacijentTermin)
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
            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
            {
                pacijentTermin.lokacija = "";
                if (prostorija.id.Equals(termin.idProstorije))
                {
                    pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                    break;
                } 
            }

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
            switch (termin.tip)
            {
                case TipTermina.OPERACIJA: pacijentTermin.napomena = "Operacija"; break;
                case TipTermina.PREGLED: pacijentTermin.napomena = "Pregled"; break;
                default: break;
            }
            pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
            pacijentTermin.id = termin.idTermina;
        }

        public void napraviAlergiju(String idPacijenta, String nazivAlergije)
        {
            Alergija alergija = new Alergija(idPacijenta, nazivAlergije);
            AlergijaServis.getInstance().dodajAlergiju(alergija);
        }
        
        public List<Alergija> procitajAlergije()
        {
            List<Alergija> alergije = new List<Alergija>();

            foreach (Alergija alergija in AlergijaServis.getInstance().ucitajSve())
            {
                if (alergija.idPacijenta.Equals(pacijent.id))
                {
                    alergije.Add(alergija);
                }

            }

            return alergije;
        }


        //obavezno refaktorisati
        public List<PacijentTermin> prikazProslihTerminaPacijentaKodOcenjivanjaLekara(String idPacijenta)
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(idPacijenta))
                {
                    if (termin.jeZavrsen)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.napomena = termin.getTipString();
                        pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.imeLekara = LekarServis.getInstance().pronadjiPunoImeLekara(termin.idLekara);
                        pacijentTermin.nazivSpecijalizacije = LekarServis.getInstance().pronadjiNazivSpecijalizacijeLekara(termin.idLekara);
                        pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);
                        pacijentTermin.nazivTerapije = TerapijaServis.getInstance().nadjiNazivLekaZaTerapiju(termin.idTerapije);

                        terminiPacijenta.Add(pacijentTermin);
                    }

                }
            }

            return terminiPacijenta;
        }

        public bool proveriStanjeAnkete(String idPacijenta)
        {
            foreach(Pacijent pacijent in pacijentRepozitorijum.ucitajSve())
            {
                if(pacijent.id.Equals(idPacijenta))
                {
                    if (pacijent.anketa)
                    {
                        return true;
                    }
                    else
                        return false;
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
                    if (pacijent.anketa)
                    {
                        pacijent.anketa = false;
                    }
                    else
                        pacijent.anketa = true;
                }

                sviPacijenti.Add(pacijent);

            }

            pacijentRepozitorijum.upisi(sviPacijenti);

        }
    }
}
