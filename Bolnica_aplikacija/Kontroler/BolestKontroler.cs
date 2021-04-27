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
        private static BolestServis bolestServis = new BolestServis();
        public static void azurirajTerapijuZaBolest(String idBolesti, String idTerapije)
        {
            bolestServis.azurirajTerapijuZaBolest(idBolesti, idTerapije);
        }

    }
}
