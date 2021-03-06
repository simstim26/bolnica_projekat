using Bolnica_aplikacija.Kontroler;
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
    class NotifikacijaServis
    {
        private NotifikacijaRepozitorijum notifikacijaRepozitorijum = new NotifikacijaRepozitorijum();
        private PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private LekarRepozitorijum lekarRepozitorijum = new LekarRepozitorijum();

        public object PacijentSer { get; private set; }

        public List<Notifikacija> prikazPacijentovihNotifikacija(String idKorisnika)
        {
            List<Notifikacija> sveNotifikacije = notifikacijaRepozitorijum.ucitajSve();
            List<Notifikacija> pacijentoveNotifikacije = new List<Notifikacija>();

            foreach(Notifikacija notifikacija in sveNotifikacije)
            {
                if(notifikacija.idKorisnika.Equals(idKorisnika))
                {
                    pacijentoveNotifikacije.Add(notifikacija);
                }
            }

            return pacijentoveNotifikacije;
        }  
    
        public void azurirajNotifikaciju(Notifikacija notifikacijaZaAzurirati)
        {
            notifikacijaRepozitorijum.azurirajNotifikaciju(notifikacijaZaAzurirati);
        }

        public Notifikacija getNotifikacija(String idNotifikacije, String idKorisnika)
        {
            Notifikacija notifikacijaPovratna = new Notifikacija();

            List<Notifikacija> sveNotifikacije = prikazPacijentovihNotifikacija(idKorisnika);

            foreach(Notifikacija notifikacija in sveNotifikacije)
            {
                if (notifikacija.id.Equals(idNotifikacije))
                {
                    notifikacijaPovratna.id = notifikacija.id;
                    notifikacijaPovratna.idKorisnika = notifikacija.idKorisnika;
                    notifikacijaPovratna.nazivNotifikacije = notifikacija.nazivNotifikacije;
                    notifikacijaPovratna.porukaNotifikacije = notifikacija.porukaNotifikacije;
                    notifikacijaPovratna.datumNotifikovanja = notifikacija.datumNotifikovanja;
                    notifikacijaPovratna.vremeNotifikovanja = notifikacija.vremeNotifikovanja;

                    break;
                }
            }

            return notifikacijaPovratna;
        }

        public bool proveriVreme(String trenutnoVreme, String trenutanDatum, String idKorisnika)
        {
            List<Notifikacija> sveNotifikacije = prikazPacijentovihNotifikacija(idKorisnika);

            foreach (Notifikacija notifikacija in sveNotifikacije)
            {
                String notifikacijaVreme = notifikacija.vremeNotifikovanja.ToString("HH:mm");
                String notifikacijaDatum = notifikacija.datumNotifikovanja.ToString("dd.MM.yyyy.");

                if (notifikacijaVreme.Equals(trenutnoVreme))
                {
                    if(notifikacijaDatum.Equals(trenutanDatum))
                    {
                        return true;
                    }
                }

            }


            return false;
        }

        public List<Notifikacija> getNoveNotifikacijeKorisnika(String idKorisnika)
        {
            List<Notifikacija> notifikacijeKorisnika = new List<Notifikacija>();

            foreach(Notifikacija notifikacija in prikazPacijentovihNotifikacija(idKorisnika))
            {
                if (!notifikacija.jeProcitana)
                {
                    notifikacijeKorisnika.Add(notifikacija);
                }
                
            }

            return notifikacijeKorisnika;

        }

        public void procitajNotifikaciju(String idNotifikacije, String idKorisnika)
        {

            foreach(Notifikacija notifikacija in prikazPacijentovihNotifikacija(idKorisnika))
            {
                if(notifikacija.id.Equals(idNotifikacije))
                {
                    oznaciKaoProcitano(notifikacija);
                    azurirajNotifikaciju(notifikacija);
                    break;
                }
            }
        }

        private void oznaciKaoProcitano(Notifikacija notifikacija)
        {
            notifikacija.jeProcitana = true;
        }

        public void napraviNotifikaciju(String nazivNotifikacije, String porukaNotifikacije, String idKorisnika, String tipKorisnika)
        {
            List<Notifikacija> notifikacije = notifikacijaRepozitorijum.ucitajSve();
            List<Pacijent> pacijenti = pacijentRepozitorijum.ucitajSve();
            List<Lekar> lekari = lekarRepozitorijum.ucitajSve();

            Notifikacija novaNotifikacija = new Notifikacija((notifikacije.Count() + 1).ToString(), nazivNotifikacije, DateTime.Now, porukaNotifikacije, idKorisnika, DateTime.Now, false);
           

            if (tipKorisnika.Equals("pacijent"))
            {
                notifikacijaZaPacijenta(pacijenti, idKorisnika, novaNotifikacija);             
            }
            else //lekar
            {
                notifikacijaZaLekara(lekari, idKorisnika, novaNotifikacija);
            }
        }

        private void notifikacijaZaPacijenta(List<Pacijent> pacijenti, String idKorisnika, Notifikacija novaNotifikacija)
        {
            foreach (Pacijent pacijent in pacijenti)
            {
                if (pacijent.id.Equals(idKorisnika))
                {

                    pacijent.Notifikacija.Add(novaNotifikacija);
                    notifikacijaRepozitorijum.dodajNotifikaciju(novaNotifikacija);

                }
            }
        }

        private void notifikacijaZaLekara(List<Lekar> lekari, String idKorisnika, Notifikacija novaNotifikacija)
        {
            foreach (Lekar lekar in lekari)
            {
                if (lekar.id.Equals(idKorisnika))
                {
                    lekar.notifikacije.Add(novaNotifikacija);
                    notifikacijaRepozitorijum.dodajNotifikaciju(novaNotifikacija);
                }
            }
        }

        public void pacijentNapraviNotifikaciju(Notifikacija notifikacija)
        {

            notifikacijaRepozitorijum.dodajNotifikaciju(notifikacija);

        }

        public List<Notifikacija> ucitajSve()
        {
            return notifikacijaRepozitorijum.ucitajSve();
        }
    }
}
