using Bolnica_aplikacija.Interfejs.Implementacija;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class ObavestenjeServis : ObavestenjeCRUDImplementacija
    {
        public void napraviObavestenje(Obavestenje obavestenje)
        {
            kreiraj(obavestenje);        
        }

        public List<Obavestenje> ucitajObavestenja()
        {
            return ucitaj();
        }

        public void azurirajObavestenje(Obavestenje obavestenjeIzmena)
        {

            azuriraj(obavestenjeIzmena);

        }

        public void obrisiObavestenje(String idObavestenja)
        {
            obrisi(idObavestenja);
        }
    }
}
