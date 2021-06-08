using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
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
        private static LekarServis instance;


        public static LekarServis getInstance()
        {
            if(instance == null)
            {
                instance = new LekarServis();
            }

            return instance;
        }

        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();

        public List<Lekar> ucitajSve()
        {
            return lekarRepozitorijum.ucitajSve();
        }
        public String pronadjiImeLekara(String idLekara)
        {
            return nadjiLekaraPoId(idLekara).prezime;
        }

        public List<PacijentTermin> pretraziSlobodneTermineZaLekara(DateTime datum, int tipAkcije)
        {
            List<PacijentTermin> povratnaVrednost = new List<PacijentTermin>();

            foreach(PacijentTermin termin in prikaziSlobodneTermineZaLekara(nadjiLekaraPoId(KorisnikServis.getInstance().getLekar().id), tipAkcije))
            {
                Termin t = TerminServis.getInstance().nadjiTerminPoId(termin.id);
                if(DateTime.Compare(t.datum.Date, datum.Date) == 0)
                {
                    povratnaVrednost.Add(termin);
                }
            }

            return povratnaVrednost;
        }

        public String pronadjiPunoImeLekara(String idLekara)
        {
            Lekar lekar = nadjiLekaraPoId(idLekara);
            return  lekar.ime + " " + lekar.prezime;
        }

        public String pronadjiNazivSpecijalizacijeLekara(String idLekara)
        {
            return SpecijalizacijaServis.getInstance().nadjiSpecijalizacijuPoId(nadjiLekaraPoId(idLekara).idSpecijalizacije);
        }

        public List<PacijentTermin> prikaziSlobodneTermineZaLekara(Lekar ulogovaniLekar, int tipAkcije)
        {
            List<PacijentTermin> slobodniTermini = new List<PacijentTermin>();
            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            { 
                if (termin.idPacijenta.Equals(""))
                {
                    DateTime trenutanDatum = DateTime.Now.AddDays(1);

                    int rezultat = DateTime.Compare(termin.datum, trenutanDatum);
                    PacijentTermin pacijentTermin = new PacijentTermin();
                    pacijentTermin.id = termin.idTermina;
                    pacijentTermin.idLekara = termin.idLekara;
                    pacijentTermin.napomena = termin.getTipString();
                    pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                    pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                    pacijentTermin.imeLekara = pronadjiImeLekara(termin.idLekara);
                    pacijentTermin.nazivSpecijalizacije = pronadjiNazivSpecijalizacijeLekara(termin.idLekara);
                    pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);

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
            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (!termin.jeZavrsen && termin.idLekara.Equals(lekar.id) && !String.IsNullOrWhiteSpace(termin.idPacijenta))
                {
                    if (DateTime.Compare(termin.datum, danasnji) >= 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                        pacijentTermin.napomena = termin.getTipString();
                        pacijentTermin.datum = termin.datum.ToString("dd.MM.yyyy.");
                        pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
                        pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);

                        terminiZaPrikaz.Add(pacijentTermin);
                    }
                }
            }
            return terminiZaPrikaz;
        }

        public List<PacijentTermin> pretraziZauzeteTermineZaLekara(Lekar lekar, DateTime prvi, DateTime drugi)
        {
            List<PacijentTermin> terminiZaPrikaz = new List<PacijentTermin>();
            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
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
                        pacijentTermin.lokacija = ProstorijaServis.getInstance().nadjiBrojISprat(termin.idProstorije);

                        terminiZaPrikaz.Add(pacijentTermin);
                    }
                }
            }
                return terminiZaPrikaz;
        }

        public List<LekarSpecijalizacija> ucitajLekareSaSpecijalizacijom()
        {
            List<LekarSpecijalizacija> povratnaVrednost = new List<LekarSpecijalizacija>();
            foreach(Lekar lekar in ucitajSve())
            {
                if (!lekar.jeLogickiObrisan)
                {
                    povratnaVrednost.Add(new LekarSpecijalizacija(lekar.id, lekar.prezime, lekar.ime,
                    SpecijalizacijaServis.getInstance().nadjiSpecijalizacijuPoId(lekar.idSpecijalizacije)));
                }
                
            }

            return povratnaVrednost;
        }
        public Lekar nadjiLekaraPoId(String idLekara)
        {
            if(idLekara == null) 
                return new Lekar();

            return ucitajSve().ToDictionary(l => l.id)[idLekara];
        }

        public Dictionary<String, String> popuniLekarComboBox(String idPacijenta)
        {
            Dictionary<string, string> lekari = new Dictionary<string, string>();
            PacijentServis.getInstance().nadjiPacijenta(idPacijenta);
            var prosliTermini = PacijentServis.getInstance().prikazProslihTerminaPacijenta(idPacijenta);

            popuniLekarDictionary(prosliTermini, lekari);

            return lekari;
        }

        private void popuniLekarDictionary(List<PacijentTermin> prosliTermini, Dictionary<String,String> lekari)
        {
            foreach (PacijentTermin pacijentTermin in prosliTermini)
            {
                Termin termin = TerminServis.getInstance().nadjiTerminPoId(pacijentTermin.id);
                Lekar lekar = LekarServis.getInstance().nadjiLekaraPoId(termin.idLekara);
                if (!lekari.ContainsKey(termin.idLekara))
                {
                    lekari.Add(termin.idLekara, lekar.ime + " " + lekar.prezime);
                }
            }
        }

        public void napraviLekara(Lekar lekar)
        {

            List<Lekar> sviLekari = lekarRepozitorijum.ucitajSve();
            lekar.id = (sviLekari.Count() + 1).ToString();

   
            lekarRepozitorijum.dodajLekara(lekar);
            KorisnikServis.getInstance().dodajKorisnika(lekar.id, lekar.korisnickoIme, lekar.lozinka, "lekar");
        }
        /*
        public List<Lekar> procitajLekare()
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
        */

        public void izmeniLekara(Lekar izmeniLekar)
        {

            lekarRepozitorijum.azurirajLekara(izmeniLekar);
            
        }

        public void obrisiLekara(String idLekara)
        {
            List<Lekar> sviLekari = lekarRepozitorijum.ucitajSve();
            foreach (Lekar lekar in sviLekari)
            {
                if (lekar.id.Equals(idLekara))
                {
                    lekar.jeLogickiObrisan = true;
                }
            }

            lekarRepozitorijum.upisi(sviLekari);
        }
    }
}
