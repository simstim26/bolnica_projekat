using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class AlergijaServis
    {
        private static AlergijaServis instance;
        private static AlergijaRepozitorijum alergijaRepozitorijum = new AlergijaRepozitorijum();

        public static AlergijaServis getInstance()
        {
            if(instance == null)
            {
                instance = new AlergijaServis();
            }

            return instance;
        }

        public void obrisiAlergiju(Alergija alergija)
        {
            List<Alergija> alergije = alergijaRepozitorijum.ucitajSve();
            foreach(Alergija a in alergije)
            {
                if(alergija.idPacijenta.Equals(a.idPacijenta) && alergija.nazivAlergije.Equals(a.nazivAlergije))
                {
                    alergije.Remove(a);
                    break;
                }
            }
            alergijaRepozitorijum.upisi(alergije);
        }
    }
}
