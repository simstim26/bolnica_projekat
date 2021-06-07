using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class PrijavaGreskeServis
    {
        private static PrijavaGreskeServis instance;
        public static PrijavaGreskeServis getInstance()
        {
            if(instance == null)
            {
                instance = new PrijavaGreskeServis();
            }

            return instance;
        }

        private PrijavaGreskeRepozitorijum prijavaGreskeRepozitorijum = new PrijavaGreskeRepozitorijum();

        public void sacuvaj(String tekst)
        {
            List<String> sveZalbe = prijavaGreskeRepozitorijum.ucitaj();
            sveZalbe.Add(tekst);
            prijavaGreskeRepozitorijum.upisi(sveZalbe);
        }
    }
}
