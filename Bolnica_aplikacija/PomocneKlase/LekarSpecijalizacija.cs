using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class LekarSpecijalizacija
    {
        public String idLekara { get; set; }
        public String imeLekara { get; set; }
        public String nazivSpecijalizacije { get; set; }

        public LekarSpecijalizacija(String idLekara, String imeLekara, String nazivSpecijalizacije)
        {
            this.idLekara = idLekara;
            this.imeLekara = imeLekara;
            this.nazivSpecijalizacije = nazivSpecijalizacije;
        }

    }
}
