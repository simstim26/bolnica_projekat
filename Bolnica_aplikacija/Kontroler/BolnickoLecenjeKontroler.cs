using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class BolnickoLecenjeKontroler
    {

        public static void napraviUputZaBolnickoLecenje(BolnickoLecenjeDTO dto)
        {
            BolnickoLecenjeServis.getInstance().napraviUputZaBolnickoLecenje(new BolnickoLecenje(dto));
        }
    }
}
