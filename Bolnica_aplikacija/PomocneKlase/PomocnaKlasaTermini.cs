using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{

    class PomocnaKlasaTermini
    {

        //TO DO: KONSULTUJ SE SA KOLEGAMA

        private List<Termin> generisaniTermini;

        public PomocnaKlasaTermini()
        {
            generisaniTermini = new List<Termin>();
        }

        public static void generisiTermine()
        {
            DateTime datum = DateTime.Now;
            Termin termin = new Termin();

            Random random = new Random();
            int randomBroj = random.Next();
            String idTermina = "T"+randomBroj.ToString();

            //satnica treba da ide kroz niz

            bool jeZavrsen = false;
            

        }

    }
}
