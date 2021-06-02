using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class BolestKontroler
    {
        public static void azurirajTerapijuZaBolest(String idBolesti, String idTerapije)
        {
            BolestServis.getInstance().azurirajTerapijuZaBolest(idBolesti, idTerapije);
        }

        public static Bolest nadjiBolestPoId(String idBolesti)
        {
            return BolestServis.getInstance().nadjiBolestPoId(idBolesti);
        }

        public static string pronadjiNazivBolestiPoId(String idBolesti)
        {
            return BolestServis.getInstance().nadjiBolestPoId(idBolesti).naziv;
        }

    }
}
