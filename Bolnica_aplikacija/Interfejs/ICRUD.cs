using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs
{
    interface ICRUD <T>
    {
        void kreiraj(T objekat);
        List<T> ucitaj();
        void azuriraj(T objekat);
        void obrisi(String id);
    }
}
