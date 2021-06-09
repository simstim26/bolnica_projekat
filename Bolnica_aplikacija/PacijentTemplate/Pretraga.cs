using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentTemplate
{
    abstract class Pretraga
    {

        public List<PacijentTermin> TemplateMetoda(String kriterijum)
        {
            return this.filtrirajTermine(kriterijum);
        }
        protected List<PacijentTermin> filtrirajTermine(String kriterijum)
        {
            List<PacijentTermin> termini = new List<PacijentTermin>();
            termini = filtrirajPoKriterijumu(kriterijum);
            return termini;
        }
        protected void popuniProstoriju(Termin termin, PacijentTermin pacijentTermin)
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
        protected void popuniLekara(Termin termin, PacijentTermin pacijentTermin, String[] podaci)
        {
            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                pacijentTermin.imeLekara = "";
                if (lekar.id.Equals(termin.idLekara) && lekar.idSpecijalizacije.Equals("0"))
                {
                    if (podaci != null)
                    {
                        if(podaci.Length == 2)
                        {
                            if(lekar.ime.ToLower().Contains(podaci[0].ToLower()) && lekar.prezime.ToLower().Contains(podaci[1].ToLower()))
                            {
                                pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                break;
                            }
                        }
                        if(podaci.Length == 1)
                        {
                            if (lekar.ime.ToLower().Contains(podaci[0].ToLower()))
                            {
                                pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                break;
                            }
                        }
                    }
                    else
                    {
                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                        break;
                    }
                }
            }
        }
        protected void popuniOstalePodatke(Termin termin, PacijentTermin pacijentTermin)
        {
            pacijentTermin.datum = termin.datum.Date.ToString("dd/MM/yyyy");
            pacijentTermin.napomena = termin.getTipString();
            pacijentTermin.satnica = termin.satnica.ToString("HH:mm");
            pacijentTermin.id = termin.idTermina;
        }
        protected void popuniPodatke(Termin termin, PacijentTermin pacijentTermin, String []podaci)
        {
            popuniProstoriju(termin, pacijentTermin);
            popuniLekara(termin, pacijentTermin, podaci);
            popuniOstalePodatke(termin, pacijentTermin);
        }
        protected abstract List<PacijentTermin> filtrirajPoKriterijumu(String kriterijum);
        protected virtual void radSaPodacimaPrilikomFiltriranja(List<PacijentTermin> termini, String kriterijum)
        {

        }

    }
}
