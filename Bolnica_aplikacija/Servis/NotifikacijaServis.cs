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
    
    }
}
