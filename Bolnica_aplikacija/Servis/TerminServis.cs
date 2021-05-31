using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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

        public  void azurirajUputTermina(String idTermina, String idUputTermina)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            Termin terminUput = nadjiTerminPoId(idUputTermina);
            termin.tipUput = terminUput.tip;
            termin.idUputTermin = terminUput.idTermina;
            termin.idUputLekara = terminUput.idLekara;
            termin.jeZavrsen = true;

            azurirajTermin(termin);

        }

        public void azurirajIzvestajUputa(String idTermina, TipTermina tip, String izvestajUputa)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.tipUput = tip;
            termin.izvestajUputa = izvestajUputa;

            azurirajTermin(termin);
        }

        public void azurirajLekaraZaUput(String idTermina, String idUputLekara)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.idUputLekara = idUputLekara;
            termin.jeZavrsen = true;

            azurirajTermin(termin);
        }

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
            /*if (idBolesti == null)
                return new Termin();

            return terminRepozitorijum.ucitajSve().GroupBy(t => t.jeZavrsen).ToDictionary(t => t.idBolesti)[idBolesti];*/
        }

        public Termin nadjiTerminPoId(String idTermina)
        {
            if (idTermina == null || idTermina == "")
                return new Termin();

            return terminRepozitorijum.ucitajSve().ToDictionary(t => t.idTermina)[idTermina];
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

        public void dodavanjeIzvestajaZaTermin(String idTermina,String nazivBolesti,String izvestajSaTermina)
        {
            BolestServis bolestServis = new BolestServis();
            TerapijaServis terapijaServis = new TerapijaServis();
            Termin termin = nadjiTerminPoId(idTermina);
            termin.izvestaj = izvestajSaTermina;
            termin.jeZavrsen = true;
            Terapija terapija = terapijaServis.nadjiTerapijuZaTermin(termin.idTermina);
            if (terapija != null)
            {
                termin.idTerapije = terapija.id;
            }
            
            termin.idBolesti = bolestServis.napraviBolest(new Bolest("",nazivBolesti, TerapijaServis.getInstance().nadjiTerapijuPoId(termin.idTerapije), PacijentServis.getInstance().nadjiPacijenta(termin.idPacijenta)));
           
            if(terapija != null)
            {
                terapijaServis.dodajIdBolestiZaTerapiju(terapija.id, termin.idBolesti);

            }
            terminRepozitorijum.azurirajTermin(termin);
        }

        public Pacijent nadjiPacijentaZaTermin(String idTermina)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            return PacijentKontroler.nadjiPacijenta(termin.idPacijenta);
        }

        public void promeniProstorijuTermina(String idTermina, String idProstorije)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.idProstorije = idProstorije;
            terminRepozitorijum.azurirajTermin(termin);
        }

        public List<Prostorija> nadjiSlobodneProstorijeZaTermin(Lekar lekar, Termin termin)
        {
            List<Prostorija> prostorijeZaPrikaz = new List<Prostorija>();
            foreach (Prostorija prostorija in ProstorijaServis.getInstance().ucitajSve())
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

            return obradiSlobodneProstorijeZaPrikaz(termin, prostorijeZaPrikaz);
        }

        private List<Prostorija> obradiSlobodneProstorijeZaPrikaz(Termin termin, List<Prostorija> prostorijeZaPrikaz)
        {
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
            Termin termin = nadjiTerminPoId(idTermina);
            return (termin.tip == TipTermina.OPERACIJA && lekar.idSpecijalizacije != "0") || termin.tip == TipTermina.PREGLED;
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

        public List<PacijentTermin> ucitajTermineZaHitanSlucaj(String tip, String idSpecijalizacije)
        {
            List<PacijentTermin> terminiUOpsegu = pronadjiTermineZaSatVremena();
            List<PacijentTermin> filtriraniTermini = pretraziTerminePoTipuISpecijalizaciji(terminiUOpsegu, tip, idSpecijalizacije);

            //Ako nema slobodnih termina, proverava zauzete
            if (filtriraniTermini.Count == 0)
            {
                List<PacijentTermin> sviTermini = PacijentServis.getInstance().ucitajZauzeteTermine();
                filtriraniTermini = pretraziTerminePoTipuISpecijalizaciji(sviTermini, tip, idSpecijalizacije);
            }

            return filtriraniTermini;
        }

        private List<PacijentTermin> pretraziTerminePoTipuISpecijalizaciji(List<PacijentTermin> termini, String tip, String idSpecijalizacije)
        {
            List<PacijentTermin> filtriraniTermini = new List<PacijentTermin>();

            foreach (PacijentTermin pacijentTermin in termini)
            {
                if (pacijentTermin.napomena.Equals(tip) && pacijentTermin.idSpecijalizacije.Equals(idSpecijalizacije))
                {
                    filtriraniTermini.Add(pacijentTermin);

                }
            }

            return filtriraniTermini;
        }

        private List<PacijentTermin> pronadjiTermineZaSatVremena()
        {
            List<PacijentTermin> datumiUOpsegu = new List<PacijentTermin>();

            foreach (PacijentTermin pacijentTermin in PacijentServis.getInstance().ucitajSlobodneTermine(0, true))
            {

                Termin termin = nadjiTerminPoId(pacijentTermin.id);
                DateTime terminDatum = termin.datum;
                DateTime datumSatUnapred = DateTime.Now.AddHours(1);

                int rezultat1 = DateTime.Compare(terminDatum, datumSatUnapred);
                int rezultat2 = DateTime.Compare(terminDatum, DateTime.Now);

                if (rezultat1 <= 0 && rezultat2 > 0)
                {
                    datumiUOpsegu.Add(pacijentTermin);
                    Console.WriteLine(pacijentTermin.napomena + " " + pacijentTermin.idSpecijalizacije);
                }
            }
           
            return datumiUOpsegu;
        }

        public void oznaciHitanTermin(String idTermina)
        {
            Termin termin = nadjiTerminPoId(idTermina);
            termin.jeHitan = true;
            azurirajTermin(termin);
        }

        public bool terminJeTokomGodisnjegOdmoraLekara(Termin termin)
        {
            bool rezultat = false;
            Lekar lekar = LekarServis.getInstance().nadjiLekaraPoId(termin.idLekara);

            if (lekar.jeNaGodisnjemOdmoru)
            {
                DateTime terminDatum = termin.datum;
                DateTime datumPocetkaGodisnjeg = lekar.pocetakGodisnjegOdmora;
                DateTime datumKrajaGodisnjeg = lekar.krajGodisnjegOdmora;

                int rezultat1 = DateTime.Compare(terminDatum, datumPocetkaGodisnjeg);
                int rezultat2 = DateTime.Compare(terminDatum, datumKrajaGodisnjeg);

                if (rezultat1 > 0 && rezultat2 < 0)
                {
                    rezultat = true;
                }
            }
            else
            {
                rezultat = false;
            }
            
            return rezultat;
        }


    }
}
