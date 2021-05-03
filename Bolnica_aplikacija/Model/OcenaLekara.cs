using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class OcenaLekara
    {
        public String idOcene { get; set; }
        public Lekar lekar { get; set; }
        public int ocena { get; set; }
        public String komentar { get; set; }
    }
}
