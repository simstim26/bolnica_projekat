using Bolnica_aplikacija.Kontroler;
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
        private TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private ProstorijaRepozitorijum prostorijaRepozitorijum = new ProstorijaRepozitorijum();
        private Termin termin; //lekar -> cuvanje izabranog termina (promena termina ili promena prostorije za izabrani termin

        public Termin nadjiTerminPoId(String idTermina)
        {
            Termin povratnaVrednost = null;
            foreach(Termin termin in terminRepozitorijum.ucitajSve())
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    povratnaVrednost = termin;
                    break;
                }
            }

            return povratnaVrednost;
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
    }
}
