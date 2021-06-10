using Bolnica_aplikacija.Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public class BolnickaSoba : ITipProstorije
    {
        public String tip { get; set; } = "Bolnicka soba";

        public bool proveriZauzetostProstorije(String id, DateTime pocetak, DateTime kraj)
        {
            foreach (BolnickoLecenje bl in BolnickoLecenjeKontroler.ucitajZaOdredjeniPeriod(pocetak, kraj))
            {
                if (bl.bolnickaSoba.Equals(id))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
