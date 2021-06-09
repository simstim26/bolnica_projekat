using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs
{
    interface IRepo <T>
    {
        List<T> ucitajSve(String putanja);

        void upisi(List<T> lista, String putanja);
    }
}
