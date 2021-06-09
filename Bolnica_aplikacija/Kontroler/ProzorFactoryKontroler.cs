using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.Kontroler
{
    class ProzorFactoryKontroler
    {
        public static Window inicijalizujProzor(String tip)
        {
            return ProzorFactoryServis.getInstance().inicijalizujProzor(tip);
        }
    }
}
