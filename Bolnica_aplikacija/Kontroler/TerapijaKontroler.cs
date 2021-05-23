using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class TerapijaKontroler
    {
        public static void azurirajTerapiju(TerapijaDTO terapijaZaAzuriranje)
        {
            TerapijaServis.getInstance().azurirajTerapiju(new Terapija(terapijaZaAzuriranje));
        }

        public static Terapija nadjiTerapijuPoId(String idTerapije)
        {
            return TerapijaServis.getInstance().nadjiTerapijuPoId(idTerapije);
        }

        public static String dodajTerapiju(TerapijaDTO terapija)
        {
            return TerapijaServis.getInstance().dodajTerapiju(new Terapija(terapija));
        }
    }
}
