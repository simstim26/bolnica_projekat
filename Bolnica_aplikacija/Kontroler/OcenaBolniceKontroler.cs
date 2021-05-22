using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class OcenaBolniceKontroler
    {
        public static List<OcenaBolnice> ucitajSve()
        {
            return OcenaBolniceServis.getInstance().ucitajSve();
        }

        public static void dodajOcenu(OcenaBolniceDTO ocenaDTO)
        {
            Pacijent pacijent = new Pacijent(ocenaDTO.pacijent.id);
            OcenaBolnice ocena = new OcenaBolnice(ocenaDTO.idOcene, ocenaDTO.ocena, ocenaDTO.komentar, pacijent);
            OcenaBolniceServis.getInstance().dodajOcenu(ocena);
        }

    }
}
