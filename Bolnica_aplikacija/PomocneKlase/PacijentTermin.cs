using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentModel
{
    public class PacijentTermin
    {
        public string id { get; set; }
        public string datum { get; set; }
        public string satnica { get; set; }
        public String lokacija { get; set; }
        public String napomena { get; set; }
        public String imeLekara { get; set; }

        public String nazivSpecijalizacije { get; set; }

    }
}
