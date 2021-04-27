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
    class LekarServis
    {
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();
        private SpecijalizacijaRepozitorijum specijalizacijaRepozitorijum = new SpecijalizacijaRepozitorijum();
        private ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();

        public List<PacijentTermin> prikaziSlobodneTermineZaLekara(Lekar ulogovaniLekar, int tipAkcije)
        {
            List<PacijentTermin> slobodniTermini = new List<PacijentTermin>();
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {
                    DateTime trenutanDatum = DateTime.Now.AddDays(1);

                    int rezultat = DateTime.Compare(termin.datum, trenutanDatum);
                    PacijentTermin pacijentTermin = new PacijentTermin();
                    pacijentTermin.id = termin.idTermina;
                    pacijentTermin.napomena = termin.getTipString();
                    pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                    String satnica = termin.satnica.ToString("HH:mm");
                    pacijentTermin.satnica = satnica;

                    foreach (Lekar lekar in lekarRepozitorijum.ucitajSve())
                    {
                        if (lekar.id.Equals(termin.idLekara))
                        {
                            pacijentTermin.imeLekara = lekar.prezime;

                            foreach (Specijalizacija spec in specijalizacijaRepozitorijum.ucitajSve())
                            {
                                if (lekar.idSpecijalizacije.Equals(spec.idSpecijalizacije))
                                {
                                    pacijentTermin.nazivSpecijalizacije = spec.nazivSpecijalizacije;
                                    break;
                                }
                            }
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
                    if (!ulogovaniLekar.idSpecijalizacije.Equals("0") && pacijentTermin.napomena.Equals("Operacija"))
                    {
                        if (tipAkcije == 0 && rezultat >= 0)
                        {
                            slobodniTermini.Add(pacijentTermin);
                        }
                        else if (tipAkcije == 1 && rezultat > 0)
                        {
                            slobodniTermini.Add(pacijentTermin);
                        }
                    }
                    else if (pacijentTermin.napomena.Equals("Pregled"))
                    {
                        if (tipAkcije == 0 && rezultat >= 0)
                        {
                            slobodniTermini.Add(pacijentTermin);
                        }
                        else if (tipAkcije == 1 && rezultat > 0)
                        {
                            slobodniTermini.Add(pacijentTermin);
                        }
                    }
                }
            }

            return slobodniTermini;
        }


        public List<PacijentTermin> prikaziZauzeteTermineZaLekara(Lekar lekar)
        {
            List<PacijentTermin> terminiZaPrikaz = new List<PacijentTermin>();
            DateTime danasnjiDatum = DateTime.Now;
            DateTime danasnji = danasnjiDatum.Date.Add(new TimeSpan(0, 0, 0));
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (!termin.jeZavrsen && termin.idLekara.Equals(lekar.id) && !termin.idPacijenta.Equals(""))
                {
                    if (DateTime.Compare(termin.datum, danasnji) >= 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                        pacijentTermin.napomena = termin.getTipString();
                        pacijentTermin.datum = termin.datum.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");

                        foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
                        {
                            if (termin.idProstorije.Equals(prostorija.id))
                            {
                                pacijentTermin.lokacija = prostorija.sprat + " " + prostorija.broj;
                                break;
                            }
                        }

                        terminiZaPrikaz.Add(pacijentTermin);
                    }
                }
            }
            return terminiZaPrikaz;
        }

        public List<PacijentTermin> pretraziZauzeteTermineZaLekara(Lekar lekar, DateTime prvi, DateTime drugi)
        {
            List<PacijentTermin> terminiZaPrikaz = new List<PacijentTermin>();
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (termin.idLekara.Equals(lekar.id) && !termin.idPacijenta.Equals(""))
                {
                    int rez1 = DateTime.Compare(termin.datum, prvi);
                    int rez2 = DateTime.Compare(termin.datum, drugi);
                    if (rez1 >= 0 && rez2 <= 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.datum = termin.datum.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.napomena = termin.getTipString(); 
                        foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
                        {
                            if (termin.idProstorije.Equals(prostorija.id))
                            {
                                pacijentTermin.lokacija = prostorija.sprat + " " + prostorija.broj;
                                break;
                            }
                        }

                        terminiZaPrikaz.Add(pacijentTermin);
                    }
                }
            }
                return terminiZaPrikaz;
        }
    }
}
