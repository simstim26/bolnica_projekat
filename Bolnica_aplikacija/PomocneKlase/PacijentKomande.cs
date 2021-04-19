using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bolnica_aplikacija.PomocneKlase
{
    public class PacijentKomande
    {
        public static RoutedUICommand Command = new RoutedUICommand("Profil komanda", "ProfilKomanda", typeof(PacijentKomande));
    }
}
