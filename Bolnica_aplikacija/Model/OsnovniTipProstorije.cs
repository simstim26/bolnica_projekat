using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public class OsnovniTipProstorije : ITipProstorije
    {
        public string tip { get; set; } = "osnovni";
    }
}
