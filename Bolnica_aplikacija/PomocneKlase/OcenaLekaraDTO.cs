using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class OcenaLekaraDTO
    {
        public String idOcene { get; set; }
        public String imeLekara { get; set; }
        public int ocena { get; set; }
        public String komentar { get; set; }

        public OcenaLekaraDTO(String idOcene, String imeLekara, int ocena, String komentar)
        {
            this.idOcene = idOcene;
            this.imeLekara = imeLekara;
            this.ocena = ocena;
            this.komentar = komentar;
        }

    }
}
