
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
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum(); //koristi se za azuriranje/prikaz termina odredjenog pacijenta
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum(); //za prikaz imena/prezimena lekara za termin odredjenog pacijenta
        private ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();
        private BolestRepozitorijum bolestRepozitorijum = new BolestRepozitorijum();
        private TerapijaRepozitorijum terapijaRepozitorijum = new TerapijaRepozitorijum();
        private SpecijalizacijaRepozitorijum specijalizacijaRepozitorijum = new SpecijalizacijaRepozitorijum();
        private LekRepozitorijum lekRepozitorijum = new LekRepozitorijum();
        private AlergijaRepozitorijum alergijaRepozitorijum = new AlergijaRepozitorijum();
        private Pacijent pacijent; //lekar -> cuva se izabrani pacijent
        private BolestTerapija bolestTerapija;

        public List<BolestTerapija> nadjiIstorijuBolestiZaPacijenta()
        {
            List<BolestTerapija> istorijaBolesti = new List<BolestTerapija>();

            foreach(Bolest bolest in bolestRepozitorijum.ucitajSve())
            {
                if (bolest.idPacijenta.Equals(pacijent.id))
                {
                    if (bolest.idTerapije == null)
                    {
                        foreach(Termin termin in terminRepozitorijum.ucitajSve())
                        {
                            if(termin.idBolesti != null && termin.idBolesti.Equals(bolest.id))
                            {
                                BolestTerapija bolestTerapija = new BolestTerapija();
                                bolestTerapija.idBolesti = bolest.id;
                                bolestTerapija.nazivBolesti = bolest.naziv;
                                bolestTerapija.idTermina = termin.idTermina;
                                bolestTerapija.izvestaj = termin.izvestaj;
                                istorijaBolesti.Add(bolestTerapija);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (Terapija terapija in terapijaRepozitorijum.ucitajSve())
                        {
                            if (terapija.idTermina != null && terapija.idBolesti.Equals(bolest.id))
                            {
                                foreach (Termin termin in terminRepozitorijum.ucitajSve())
                                {
                                    if (termin.idBolesti != null && termin.idBolesti.Equals(terapija.idBolesti))
                                    {
                                        foreach (Lek lek in lekRepozitorijum.ucitajSve())
                                        {
                                            if (lek.id.Equals(terapija.idLeka))
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
                                                break;
                                            }

                                        }
                                        break;
                                    }
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
            foreach (Bolest bolest in bolestRepozitorijum.ucitajSve())
            {
                if (bolest.idPacijenta.Equals(pacijent.id))
                {
                    foreach (Terapija terapija in terapijaRepozitorijum.ucitajSve())
                    {
                        if(terapija.idTermina == null && terapija.idBolesti.Equals(bolest.id))
                        {
                            foreach (Lek lek in lekRepozitorijum.ucitajSve())
                            {
                                if (lek.id.Equals(terapija.idLeka))
                                {
                                    BolestTerapija bolestTerapija = new BolestTerapija();
                                    bolestTerapija.idBolesti = bolest.id;
                                    bolestTerapija.nazivBolesti = bolest.naziv;
                                    bolestTerapija.idTerapije = terapija.id;
                                    bolestTerapija.nazivTerapije = lek.naziv;
                                    bolestTerapija.idLeka = lek.id;
                                    bolestTerapija.kolicina = lek.kolicina.ToString();
                                    povratnaVrednost.Add(bolestTerapija);
                                    break;
                                }

                            }
                        }
                        if (terapija.idTermina != null && terapija.idBolesti.Equals(bolest.id))
                        {
                            foreach (Termin termin in terminRepozitorijum.ucitajSve())
                            {
                                if (termin.idBolesti != null && termin.idBolesti.Equals(terapija.idBolesti))
                                {
                                    foreach (Lek lek in lekRepozitorijum.ucitajSve())
                                    {
                                        if (lek.id.Equals(terapija.idLeka))
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
                                            povratnaVrednost.Add(bolestTerapija);
                                            break;
                                        }

                                    }
                                    break;
                                }
                            }
                        }

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
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idStarogTermina.Equals(termin.idTermina)) //otkazivanje starog termina
                {
                    termin.idPacijenta = "";
                    terminRepozitorijum.azurirajTermin(termin);
                }

                if (idNovogTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = pacijent.id;
                    terminRepozitorijum.azurirajTermin(termin); 
                }
            }

        }

        public List<PacijentTermin> prikazPacijentovihTermina()
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if(termin.idPacijenta.Equals(pacijent.id))
                {
                    DateTime terminDatum = termin.datum;
                    DateTime danasnjiDatum = DateTime.Today;

                    int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                    if (rezultat > 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
                        {
                            if (prostorija.id.Equals(termin.idProstorije))
                            {
                                pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", sala broj " + prostorija.broj;
                                break;
                            }
                        }

                        foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                        {
                            if (lekar.id.Equals(termin.idLekara))
                            {
                                pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                pacijentTermin.idSpecijalizacije = lekar.idSpecijalizacije;
                                break;
                            }
                        }

                        pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
                        switch(termin.tip)
                        {
                            case TipTermina.OPERACIJA:  pacijentTermin.napomena = "Operacija"; break;
                            case TipTermina.PREGLED:    pacijentTermin.napomena = "Pregled"; break;
                            default:                    break;
                        }
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.id = termin.idTermina;

                        

                        terminiPacijenta.Add(pacijentTermin);
                    }
                }
            }

            return terminiPacijenta;
        }

        public List<PacijentTermin> prikazSvihTerminaPacijenta()
        {
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();

            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (termin.idPacijenta.Equals(pacijent.id))
                {
                    PacijentTermin pacijentTermin = new PacijentTermin();
                    pacijentTermin.id = termin.idTermina;
                    pacijentTermin.napomena = termin.getTipString();
                    pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                    pacijentTermin.satnica = termin.satnica.ToString("HH:mm");

                    foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                    {
                        if (lekar.id.Equals(termin.idLekara))
                        {
                            pacijentTermin.imeLekara = lekar.prezime;
                            break;
                        }
                    }

                    foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
                    {
                        if (termin.idProstorije.Equals(prostorija.id))
                        {
                            pacijentTermin.lokacija = prostorija.sprat + " " + prostorija.broj;
                            break;
                        }
                    }

                    terminiPacijenta.Add(pacijentTermin);
                }
            }

            return terminiPacijenta;
        }

        public void zakaziTerminPacijentu(String idTermina)
        {
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = pacijent.id;
                    terminRepozitorijum.azurirajTermin(termin);
                    break;
                }
            }
        }
        public void otkaziTerminPacijenta(String idTermina)
        {
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                    terminRepozitorijum.azurirajTermin(termin);
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
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();

            int jeZakazivanje = indikator;

            if(jeZakazivanje == 0)
            {
                foreach (Termin termin in terminRepozitorijum.ucitajSve())
                {
                    if (termin.idPacijenta.Equals(""))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime danasnjiDatum = DateTime.Today;
                        int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                        if (rezultat > 0)
                        {
                            PacijentTermin pacijentTermin = new PacijentTermin();
                            foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
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

                            foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                            {
                                if (lekar.id.Equals(termin.idLekara))
                                {
                                    if (lekar.idSpecijalizacije.Equals("0") || jeSekretar)
                                    {
                                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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

                                terminiSlobodni.Add(pacijentTermin);
                            }

                        }
                    }
                }
            }
            else
            {
                foreach (Termin termin in terminRepozitorijum.ucitajSve())
                {
                    if (termin.idPacijenta.Equals(""))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime danasnjiDatum = DateTime.Today;
                        DateTime gornjaGranicaDatuma = DateTime.Today.AddDays(3);
                        int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);
                        int rezultatGornji = DateTime.Compare(terminDatum, gornjaGranicaDatuma);
                        int rezultatDodatni = DateTime.Compare(danasnjiDatum.AddDays(1), terminDatum);

                        //rezultat je za poredjenje danasnjeg datuma i terminovog datuma, 
                        //rezultatGornji proverava da li je datum unutar ogranicenja od 3 dana za pomeranje
                        //rezultatDodatni proverava da li je termin dan posle danasnjeg jer ukoliko jeste ne bi trebalo da sme da se pomeri na taj datum

                        if (rezultat > 0 && rezultatGornji < 0 && rezultatDodatni < 0)
                        {
                            PacijentTermin pacijentTermin = new PacijentTermin();
                            foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
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

                            foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                            {
                                if (lekar.id.Equals(termin.idLekara))
                                {
                                    if (lekar.idSpecijalizacije.Equals("0") || jeSekretar)
                                    {
                                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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

                                terminiSlobodni.Add(pacijentTermin);
                            }

                        }
                    }
                }
            }

            

            return terminiSlobodni;
        }

        public List<PacijentTermin> filtrirajTermine(int indikator, String kriterijum)
        {
            //ako je indikator 0 onda se pretrazuje tj filtrira tabela u odnosu na lekare
            //ako je indikator 1 onda se radi to u odnosu na vreme odrzavanja 

            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();

            //pretraga po lekarevom imenu 
            if(indikator == 0)
            {

                String[] podaci = kriterijum.Split(' ');

                foreach (Termin termin in terminRepozitorijum.ucitajSve())
                {
                    if (termin.idPacijenta.Equals(""))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime danasnjiDatum = DateTime.Today;
                        int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                        if (rezultat > 0)
                        {
                            PacijentTermin pacijentTermin = new PacijentTermin();
                            foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
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

                            foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                            {
                                if (podaci.Length == 2)
                                {
                                    if (lekar.id.Equals(termin.idLekara) && lekar.ime.ToLower().Contains(podaci[0].ToLower()) && lekar.prezime.ToLower().Contains(podaci[1].ToLower()))
                                    {
                                        if (lekar.idSpecijalizacije.Equals("0"))
                                        {
                                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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
                                else if (podaci.Length == 1)
                                {
                                    if (lekar.id.Equals(termin.idLekara) && lekar.ime.ToLower().Contains(podaci[0].ToLower()))
                                    {
                                        if (lekar.idSpecijalizacije.Equals("0"))
                                        {
                                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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
                                else
                                    pacijentTermin.imeLekara = "";
                                
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

                                terminiSlobodni.Add(pacijentTermin);
                            }

                        }
                    }
                }
            }
            else if (indikator == 1) //pretraga po vremenu odrzavanja
            {
                DateTime kriterijumPretrage = new DateTime();

                try
                {
                    kriterijumPretrage = Convert.ToDateTime(kriterijum);

                    foreach (Termin termin in terminRepozitorijum.ucitajSve())
                    {
                        if (termin.idPacijenta.Equals(""))
                        {
                            DateTime terminDatum = termin.datum;
                            DateTime danasnjiDatum = DateTime.Today;
                            int rezultat = DateTime.Compare(terminDatum, danasnjiDatum);

                            int rezultatPretrage = DateTime.Compare(terminDatum, kriterijumPretrage);

                            if (rezultat > 0 && rezultatPretrage == 0)
                            {
                                PacijentTermin pacijentTermin = new PacijentTermin();
                                foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
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

                                foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                                {


                                    if (lekar.id.Equals(termin.idLekara))
                                    {
                                        if (lekar.idSpecijalizacije.Equals("0"))
                                        {
                                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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

                                    terminiSlobodni.Add(pacijentTermin);
                                }

                            }
                        }
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return terminiSlobodni;
        }

        public void napraviAlergiju(String idPacijenta, String nazivAlergije)
        {
            Alergija alergija = new Alergija(idPacijenta, nazivAlergije);
            alergijaRepozitorijum.dodajAlergiju(alergija);
        }
        
        public List<Alergija> procitajAlergije()
        {
            List<Alergija> alergije = new List<Alergija>();

            foreach (Alergija alergija in alergijaRepozitorijum.ucitajSve())
            {
                if (alergija.idPacijenta.Equals(pacijent.id))
                {
                    alergije.Add(alergija);
                }

            }

            return alergije;
        }
    }
}
