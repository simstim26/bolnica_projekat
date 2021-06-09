using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public class SobaZaPregled : ITipProstorije
    {
        public String tip { get; set; } = "Soba za pregled";
    }
}
