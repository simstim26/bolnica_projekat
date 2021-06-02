using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class BolestServis
    {
        private static BolestServis instance;
        public static BolestServis getInstance()
        {
            if(instance == null)
            {
                instance = new BolestServis();
            }
            return instance;
        }
        private BolestRepozitorijum bolestRepozitorijum = new BolestRepozitorijum();


        public Bolest nadjiBolestPoId(String id)
        {
            if (id == null)
                return new Bolest();

            return bolestRepozitorijum.ucitajSve().ToDictionary(b => b.id)[id];
        }

        public List<Bolest> ucitajSve()
        {
            return bolestRepozitorijum.ucitajSve();
        }
        public void azurirajTerapijuZaBolest(String idBolesti, String idTerapije)
        {
            Bolest bolest = nadjiBolestPoId(idBolesti);
            bolest.terapija.id = idTerapije;
            bolestRepozitorijum.azurirajBolest(bolest);
        }
        public String napraviBolest(Bolest bolest)
        {
            var sveBolesti = bolestRepozitorijum.ucitajSve();
            bolest.id = (sveBolesti.Count + 1).ToString();
            sveBolesti.Add(bolest);
            bolestRepozitorijum.upisi(sveBolesti);

            return bolest.id;
        }

    }
}
