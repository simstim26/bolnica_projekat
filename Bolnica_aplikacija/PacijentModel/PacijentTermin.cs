using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentModel
{
    public class PacijentTermin
    {
        public DateTime datum { get; set; }
        public DateTime satnica { get; set; }
        public String brojProstorije { get; set; }
        public String napomena { get; set; }
        public String imeLekara { get; set; }

    }
}
