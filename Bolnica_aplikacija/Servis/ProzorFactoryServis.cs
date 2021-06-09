using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentStudent;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.Servis
{
    class ProzorFactoryServis
    {
        private static ProzorFactoryServis instance;
        public static ProzorFactoryServis getInstance()
        {
            if (instance == null)
            {
                instance = new ProzorFactoryServis();
            }

            return instance;
        }

        public Window inicijalizujProzor(String tip)
        {
            Window prozor = null;                     
            
            if (tip.Equals("pacijent"))
            {
                prozor = new ProzorPacijent();
            }
            else if (tip.Equals("lekar"))
            {
                prozor = new LekarProzor();
            }
            else if (tip.Equals("sekretar"))
            {
                Console.WriteLine("Pravim prozor za sekretara");
                prozor = new SekretarProzor();
            }
            else if (tip.Equals("upravnik"))
            {
               prozor = UpravnikProzor.getInstance();
            }

            return prozor;

        }
    }
}
