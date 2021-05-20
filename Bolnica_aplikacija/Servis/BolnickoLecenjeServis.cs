using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class BolnickoLecenjeServis
    {
        BolnickoLecenjeRepozitorijum bolnickoLecenjeRepozitorijum = new BolnickoLecenjeRepozitorijum();
        private static BolnickoLecenjeServis instance = null;

        public static BolnickoLecenjeServis getInstance()
        {
            if(instance == null)
            {
                instance = new BolnickoLecenjeServis();
            }

            return instance;
        }

        public void napraviUputZaBolnickoLecenje(BolnickoLecenje bolnickoLecenje)
        {
            List<BolnickoLecenje> sviUputi = bolnickoLecenjeRepozitorijum.ucitajSve();
            bolnickoLecenje.id = (sviUputi.Count + 1).ToString();
            sviUputi.Add(bolnickoLecenje);
            bolnickoLecenjeRepozitorijum.upisi(sviUputi);
            
        }

    }
}
