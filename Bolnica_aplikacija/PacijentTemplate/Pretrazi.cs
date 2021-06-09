using Bolnica_aplikacija.PacijentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentTemplate
{
    class Pretrazi
    {
        public static List<PacijentTermin> izvrsiFiltriranje(Pretraga pretraga, String kriterijum)
        {
            return pretraga.TemplateMetoda(kriterijum);
        }
    }
}
