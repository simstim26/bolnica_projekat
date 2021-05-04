using Bolnica_aplikacija.Kontroler;
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
    class TerminServis
    {
        private static TerminServis instance;
        public static TerminServis getInstance()
        {
            if(instance == null)
            {
                instance = new TerminServis();
            }
            return instance;
        }
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();
        private Termin termin; //lekar -> cuvanje izabranog termina (promena termina ili promena prostorije za izabrani termin

        public void azurirajTermin(Termin terminZaAzuriranje) //da li ide u servis ??
        {
            terminRepozitorijum.azurirajTermin(terminZaAzuriranje);
        }

        public Termin nadjiTerminZaBolest(String idBolesti)
        {
            Termin povratnaVrednost = null;
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idBolesti.Equals(termin.idBolesti))
                {
                    povratnaVrednost = termin;
                    break;
                }
            }

            return povratnaVrednost;
        }

        public Termin nadjiTerminPoId(String idTermina)
        {
            Termin povratnaVrednost = new Termin();
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina != null && idTermina.Equals(termin.idTermina))
                {
                    povratnaVrednost.kopiraj(termin);
                    break;
                }
            }

            return povratnaVrednost;
        }

        public void azuriranjeTerapijeZaTermin(String idTermina, String idTerapija)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.idTerapije = idTerapija;
            terminRepozitorijum.azurirajTermin(termin);
        }
        public void azuriranjeIzvestajaZaTermin(String azuriraniIzvestaj, String idTermina)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.izvestaj = azuriraniIzvestaj;
            terminRepozitorijum.azurirajTermin(termin);
        }

        public void dodavanjeIzvestajaZaTermin(String nazivBolesti,String izvestajSaTermina)
        {
            BolestServis bolestServis = new BolestServis();
            TerapijaServis terapijaServis = new TerapijaServis();
            termin.izvestaj = izvestajSaTermina;
            termin.jeZavrsen = true;
            Terapija terapija = terapijaServis.nadjiTerapijuZaTermin(termin.idTermina);
            if (terapija != null)
            {
                termin.idTerapije = terapija.id;
            }
            
            termin.idBolesti = bolestServis.napraviBolest(nazivBolesti, termin.idPacijenta, termin.idTerapije);
           
            if(terapija != null)
            {
                terapijaServis.dodajIdBolestiZaTerapiju(terapija.id, termin.idBolesti);

            }
            terminRepozitorijum.azurirajTermin(termin);
        }

        public void nadjiPacijentaZaTermin(String idTermina)
        {
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    PacijentKontroler.nadjiPacijenta(termin.idPacijenta);
                   
                    break;
                }
            }
        }

        public void promeniProstorijuTermina(String idTermina, String idProstorije)
        {
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idProstorije = idProstorije;
                    terminRepozitorijum.azurirajTermin(termin);
                    break;
                }
            }
        }

        public List<Prostorija> nadjiSlobodneProstorijeZaTermin(Lekar lekar, Termin termin)
        {
            List<Prostorija> prostorijeZaPrikaz = new List<Prostorija>();
            foreach (Prostorija prostorija in prostorijaRepozitorijum.ucitajSve())
            {
                if (!prostorija.logickiObrisana && prostorija.dostupnost)
                {
                    if (lekar.idSpecijalizacije != "0" && prostorija.tipProstorije != TipProstorije.BOLNICKA_SOBA && prostorija.tipProstorije != TipProstorije.GRESKA)
                    {
                        if (termin.tip == TipTermina.OPERACIJA && prostorija.tipProstorije == TipProstorije.OPERACIONA_SALA)
                        {
                            prostorijeZaPrikaz.Add(prostorija);
                        }
                        else if (termin.tip == TipTermina.PREGLED && prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                        {
                            prostorijeZaPrikaz.Add(prostorija);
                        }
                    }
                    else if (lekar.idSpecijalizacije == "0" && prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                    {
                        prostorijeZaPrikaz.Add(prostorija);
                    }
                }
            }

            List<String> nePrikazati = proveriProstorijeZaPrikaz(termin, prostorijeZaPrikaz);
            List<Prostorija> povratnaVrednost = new List<Prostorija>();
            bool zastavica;
            foreach (Prostorija prostorija in prostorijeZaPrikaz)
            {
                zastavica = false;
                foreach (String idProstorije in nePrikazati)
                {
                    if (idProstorije.Equals(prostorija.id))
                    {
                        zastavica = true;
                        break;
                    }
                }

                if (!zastavica)
                {
                    povratnaVrednost.Add(prostorija);
                }

            }

            return povratnaVrednost;
        }

        private List<String> proveriProstorijeZaPrikaz(Termin termin,List<Prostorija> prostorijeZaPrikaz)
        {
            List<String> povratnaVrednost = new List<String>();

            povratnaVrednost.Add(termin.idProstorije);
            foreach (Termin temp in terminRepozitorijum.ucitajSve())
            {
                if (DateTime.Compare(termin.datum, temp.datum) == 0)
                {
                    if (TimeSpan.Compare(termin.satnica.TimeOfDay, temp.satnica.TimeOfDay) == 0)
                    {
                        povratnaVrednost.Add(temp.idProstorije);
                    }

                }

            }
            return povratnaVrednost;
        }
        public bool proveriTipTermina(Lekar lekar, String idTermina)
        {
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    if (termin.tip == TipTermina.OPERACIJA && lekar.idSpecijalizacije != "0")
                    {
                        return true;
                    }
                    if(termin.tip == TipTermina.PREGLED)
                    {
                        return true;
                    }
                }
            }

                return false;
        }
        public void sacuvajTermin(String idTermina)
        {
            termin = nadjiTerminPoId(idTermina);
        }

        public Termin getTermin()
        {
            return termin;
        }


        public String nadjiIdLekaraZaTermin(String idTermina)
        {
            String idNadjenogLekara = "";
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    idNadjenogLekara = termin.idLekara;

                    break;
                }
            }
            return idNadjenogLekara;
        }

        public List<Termin> ucitajSve()
        {
            return terminRepozitorijum.ucitajSve();

        }

        public List<PacijentTermin> ucitajPregledaZaIzabranogLekara(String idLekara)
        {
            List<PacijentTermin> povratnaVrednost = new List<PacijentTermin>();
            foreach(PacijentTermin termin in LekarServis.getInstance().prikaziSlobodneTermineZaLekara(LekarServis.getInstance().nadjiLekaraPoId(idLekara), 0))
            {
                if (termin.idLekara.Equals(idLekara) && termin.napomena.Equals("Pregled"))
                {
                    povratnaVrednost.Add(termin);
                }
            }

            return povratnaVrednost;
        }

        public String napraviTermin(Termin termin)
        {
            termin.idTermina = (ucitajSve().Count + 1).ToString();
            terminRepozitorijum.dodajTermin(termin);
            return termin.idTermina;
        }

        public void veziTermin(String idTerminUput)
        {
            Termin terminUput = nadjiTerminPoId(idTerminUput);
            termin.idUputLekara = terminUput.idLekara;
            termin.idUputTermin = terminUput.idTermina;
            termin.jeZavrsen = true;
            terminRepozitorijum.azurirajTermin(termin);
        }

        public List<PacijentTermin> ucitajTermineZaHitanSlucaj(String tip, String idSpecijalizacije)
        {

            List<PacijentTermin> datumiUOpsegu = pronadjiTermineZaSatVremena();
            List<PacijentTermin> filtriraniTermini = new List<PacijentTermin>();

            foreach (PacijentTermin pacijentTermin in datumiUOpsegu)
            {
                if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                {
                    filtriraniTermini.Add(pacijentTermin);
                }
            }

            //Ako nema slobodnih termina, proverava zauzete
            if (filtriraniTermini.Count == 0)
            {
                List<PacijentTermin> sviTermini = PacijentServis.getInstance().ucitajZauzeteTermine();

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

        public List<PacijentTermin> pronadjiTermineZaSatVremena()
        {
            List<PacijentTermin> datumiUOpsegu = new List<PacijentTermin>();
            foreach (Termin termin in terminRepozitorijum.ucitajSve())
            {
                foreach (PacijentTermin pacijentTermin in PacijentServis.getInstance().ucitajSlobodneTermine(0, true))
                {
                    if (pacijentTermin.id.Equals(termin.idTermina))
                    {
                        DateTime terminDatum = termin.datum;
                        DateTime datumSatUnapred = DateTime.Now.AddHours(1);

                        int rezultat1 = DateTime.Compare(terminDatum, datumSatUnapred);
                        int rezultat2 = DateTime.Compare(terminDatum, DateTime.Now);

                        if (rezultat1 <= 0 && rezultat2 > 0)
                        {
                            datumiUOpsegu.Add(pacijentTermin);
                        }

                    }
                }
            }

            return datumiUOpsegu;
        }


    }
}
