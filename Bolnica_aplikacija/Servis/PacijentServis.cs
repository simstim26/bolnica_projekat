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
                    termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                    terminRepozitorijum.azurirajTermin(termin); 
                }
            }

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
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
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
    }
}
