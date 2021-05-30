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
        public String prezimeLekara { get; set; }
        public String nazivSpecijalizacije { get; set; }

        public LekarSpecijalizacija(String idLekara, String prezimeLekara, String imeLekara, String nazivSpecijalizacije)
        {
            this.idLekara = idLekara;
            this.imeLekara = imeLekara;
            this.prezimeLekara = prezimeLekara;
            this.nazivSpecijalizacije = nazivSpecijalizacije;
        }

    }
}
