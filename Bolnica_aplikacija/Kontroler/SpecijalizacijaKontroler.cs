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
        public static String nadjiSpecijalizacijuPoId(String id)
        {
            return SpecijalizacijaServis.getInstance().nadjiSpecijalizacijuPoId(id);
        }
    }
}
