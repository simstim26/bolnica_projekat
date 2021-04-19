using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class PacijentServis
    {
        private PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum(); //koristi se za azuriranje/prikaz termina odredjenog pacijenta
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum(); //za prikaz imena/prezimena lekara za termin odredjenog pacijenta
        private ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();
        private SpecijalizacijaRepozitorijum specijalizacijaRepozitorijum = new SpecijalizacijaRepozitorijum();
        private Pacijent pacijent; //lekar -> cuva se izabrani pacijent
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
        
    }
}
