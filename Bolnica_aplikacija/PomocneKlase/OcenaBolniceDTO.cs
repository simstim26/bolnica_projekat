using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class OcenaBolniceDTO
    {
        public String idOcene { get; set; }
        public int ocena { get; set; }
        public String komentar { get; set; }
        public PacijentDTO pacijent { get; set; }

        public OcenaBolniceDTO(String idOcene, int ocena, String komentar, PacijentDTO pacijent)
        {
            this.idOcene = idOcene;
            this.ocena = ocena;
            this.komentar = komentar;
            this.pacijent = pacijent;
        }

    }
}
