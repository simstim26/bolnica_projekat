using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentModel
{
    public class PacijentTermin
    {
        public String id { get; set; }
        public String datum { get; set; }
        public String satnica { get; set; }
        public String lokacija { get; set; }
        public String napomena { get; set; }
        public String imeLekara { get; set; }

        public String nazivSpecijalizacije { get; set; }

        public String idSpecijalizacije { get; set; }

    }
}
