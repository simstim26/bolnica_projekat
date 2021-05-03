using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class LogovanjeKontroler
    {
        public static List<Logovanje> ucitajSve()
        {
            return LogovanjeServis.getInstance().ucitajSve();
        }

        public static void dodajLogovanje(Logovanje logovanje)
        {
            LogovanjeServis.getInstance().dodajLogovanje(logovanje);
        }

        public static bool proveriPostojanjeLogovanja(String idKorisnika)
        {
            return LogovanjeServis.getInstance().proveriPostojanjeLogovanja(idKorisnika);
        }

        public static bool proveriVremePostojecegLogovanja(String idKorisnika)
        {
            return LogovanjeServis.getInstance().proveriVremePostojecegLogovanja(idKorisnika);
        }

        public static void resetujLogovanje(String idKorisnika)
        {
            LogovanjeServis.getInstance().resetujLogovanje(idKorisnika);
        }

        public static void uvecajBrojIzmena(String idKorisnika)
        {
            LogovanjeServis.getInstance().uvecajBrojIzmena(idKorisnika);
        }

        public static int getBrojUzastopnihPonavljanja(String idKorisnika)
        {
            return LogovanjeServis.getInstance().getBrojUzastopnihIzmena(idKorisnika);
        }

        public static DateTime getVremeIzmene(String idKorisnika)
        {
            return LogovanjeServis.getInstance().getVremeIzmene(idKorisnika);
        }

    }
}
