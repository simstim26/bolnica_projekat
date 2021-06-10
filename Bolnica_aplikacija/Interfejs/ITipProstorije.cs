using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    public interface ITipProstorije
    {
        String tip { get; set; }

        bool proveriZauzetostProstorije(String id, DateTime pocetak, DateTime kraj);
    }
}
