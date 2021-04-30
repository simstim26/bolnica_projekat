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
        public static List<PacijentTermin> prikaziSlobodneTermineZaLekara(Lekar ulogovaniLekar, int tipAkcije)
        {
            return LekarServis.getInstance().prikaziSlobodneTermineZaLekara(ulogovaniLekar, tipAkcije);
        }

        public static List<PacijentTermin> prikaziZauzeteTermineZaLekara(Lekar ulogovaniLekar)
        {
            return LekarServis.getInstance().prikaziZauzeteTermineZaLekara(ulogovaniLekar);
        }
        public static List<PacijentTermin> pretraziZauzeteTermineZaLekara(Lekar lekar, DateTime prvi, DateTime drugi)
        {
            return LekarServis.getInstance().pretraziZauzeteTermineZaLekara(lekar, prvi, drugi);
        }
        public static List<Lekar> ucitajSve()
        {
            return LekarServis.getInstance().ucitajSve();
        }
    }
}
