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
        private PacijentServis pacijentServis = new PacijentServis();

        public List<Notifikacija> prikazPacijentovihNotifikacija()
        {
            List<Notifikacija> sveNotifikacije = notifikacijaRepozitorijum.ucitajSve();
            List<Notifikacija> pacijentoveNotifikacije = new List<Notifikacija>();

            Pacijent pacijent = pacijentServis.getPacijent();

            foreach(Notifikacija notifikacija in sveNotifikacije)
            {
                if(notifikacija.idKorisnika.Equals(pacijent.id))
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

        public Notifikacija getNotifikacija(String idNotifikacije)
        {
            Notifikacija notifikacijaPovratna = new Notifikacija();

            List<Notifikacija> sveNotifikacije = prikazPacijentovihNotifikacija();

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

        public bool proveriVreme(String trenutnoVreme, String trenutanDatum)
        {
            List<Notifikacija> sveNotifikacije = prikazPacijentovihNotifikacija();

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

            foreach(Notifikacija notifikacija in prikazPacijentovihNotifikacija())
            {
                if (!notifikacija.jeProcitana)
                {
                    notifikacijeKorisnika.Add(notifikacija);
                }
                
            }

            return notifikacijeKorisnika;

        }

        public void procitajNotifikaciju(String idNotifikacije)
        {
            Notifikacija procitanaNotifikacija = new Notifikacija();

            foreach(Notifikacija notifikacija in prikazPacijentovihNotifikacija())
            {
                if(notifikacija.id.Equals(idNotifikacije))
                {
                    procitanaNotifikacija.id = idNotifikacije;
                    procitanaNotifikacija.idKorisnika = notifikacija.idKorisnika;
                    procitanaNotifikacija.jeProcitana = true;
                    procitanaNotifikacija.nazivNotifikacije = notifikacija.nazivNotifikacije;
                    procitanaNotifikacija.porukaNotifikacije = notifikacija.porukaNotifikacije;

                    break;
                }
            }

            azurirajNotifikaciju(procitanaNotifikacija);

        }

    }
}
