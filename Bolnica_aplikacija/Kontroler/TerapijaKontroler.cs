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
        public static void azurirajTerapiju(Terapija terapijaZaAzuriranje)
        {
            TerapijaServis.getInstance().azurirajTerapiju(terapijaZaAzuriranje);
        }

        public static Terapija nadjiTerapijuPoId(String idTerapije)
        {
            return TerapijaServis.getInstance().nadjiTerapijuPoId(idTerapije);
        }

        public static String dodajTerapiju(Terapija terapija)
        {
            return TerapijaServis.getInstance().dodajTerapiju(terapija);
        }
    }
}
