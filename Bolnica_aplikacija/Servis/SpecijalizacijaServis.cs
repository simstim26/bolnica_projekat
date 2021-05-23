using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class SpecijalizacijaServis
    {
        private static SpecijalizacijaServis instance;

        public static SpecijalizacijaServis getInstance()
        {
            if(instance == null)
            {
                instance = new SpecijalizacijaServis();
            }
            return instance;
        }

        private SpecijalizacijaRepozitorijum specijalizacijaRepozitorijum = new SpecijalizacijaRepozitorijum();

        public String nadjiSpecijalizacijuPoId(String id)
        {
            if (id == null)
                return "";

            String povratnaVrednost = "";

            foreach(Specijalizacija specijalizacija in specijalizacijaRepozitorijum.ucitajSve())
            {
                if (id.Equals(specijalizacija.idSpecijalizacije))
                {
                    povratnaVrednost = specijalizacija.nazivSpecijalizacije;
                    break;
                }
            }

            return povratnaVrednost;
        }
    }
}
