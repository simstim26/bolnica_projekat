using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class SpecijalizacijaKontroler
    {
        private static SpecijalizacijaServis specijalizacijaServis = new SpecijalizacijaServis();
        public static String nadjiSpecijalizacijuPoId(String id)
        {
            return specijalizacijaServis.nadjiSpecijalizacijuPoId(id);
        }

    }
}
