using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class PacijentKontroler
    {
        private static PacijentServis pacijentServis = new PacijentServis();

        public static List<Pacijent> prikazPacijenata() //prikaz pacijenata kod lekara
        {
            return pacijentServis.prikazPacijenata();
        }

    }
}
