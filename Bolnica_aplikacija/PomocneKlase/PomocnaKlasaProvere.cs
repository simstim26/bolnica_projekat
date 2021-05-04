using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class PomocnaKlasaProvere
    {
        //ako je trenutan datum manji od datuma za uporediti onda je povratna vrednost false u suprotnom je true
        public static bool uporediDatumSaDanasnjim(DateTime datumZaUporediti)
        {
            bool povratnaVrednost = false;

            int rezultat = DateTime.Compare(DateTime.Now, datumZaUporediti.AddHours(1));

            if(rezultat <= 0)
            {
                povratnaVrednost = false;
            }
            else
            {
                povratnaVrednost = true;
            }

            return povratnaVrednost;
        }

    }
}
