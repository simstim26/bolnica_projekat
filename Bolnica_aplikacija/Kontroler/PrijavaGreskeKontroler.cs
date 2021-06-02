using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class PrijavaGreskeKontroler
    {

        public static void sacuvaj(String tekst)
        {
            PrijavaGreskeServis.getInstance().sacuvaj(tekst);
        }
    }
}
