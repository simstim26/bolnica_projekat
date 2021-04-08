using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class LekarKontroler
    {
        private static LekarServis lekarServis = new LekarServis();

        public static List<PacijentTermin> prikaziSlobodneTermineZaLekara(Lekar ulogovaniLekar, int tipAkcije)
        {
            return lekarServis.prikaziSlobodneTermineZaLekara(ulogovaniLekar, tipAkcije);
        }

        public static List<PacijentTermin> prikaziZauzeteTermineZaLekara(Lekar ulogovaniLekar)
        {
            return lekarServis.prikaziZauzeteTermineZaLekara(ulogovaniLekar);
        }
        public static List<PacijentTermin> pretraziZauzeteTermineZaLekara(Lekar lekar, DateTime prvi, DateTime drugi)
        {
            return lekarServis.pretraziZauzeteTermineZaLekara(lekar, prvi, drugi);
        }

    }
}
