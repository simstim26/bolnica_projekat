using Bolnica_aplikacija.Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public class OperacionaSala : ITipProstorije
    {
        public String tip { get; set; } = "Operaciona sala";

        public bool proveriZauzetostProstorije(String id, DateTime pocetak, DateTime kraj)
        {
            foreach (Termin t in TerminKontroler.ucitajSve())
            {
                
            }
            return true;
        }
    }
}
