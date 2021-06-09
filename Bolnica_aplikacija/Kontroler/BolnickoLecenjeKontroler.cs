using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class BolnickoLecenjeKontroler
    {

        public static void napraviUputZaBolnickoLecenje(BolnickoLecenjeDTO dto)
        {
            BolnickoLecenjeServis.getInstance().napraviUputZaBolnickoLecenje(new BolnickoLecenje(dto));
        }

        public static bool proveriBolnickoLecenjeZaPacijenta(String idPacijenta)
        {
            return BolnickoLecenjeServis.getInstance().proveriBolnickoLecenjeZaPacijenta(idPacijenta);
        }

        public static BolnickoLecenje nadjiBolnickoLecenjeZaPacijenta(String idPacijenta)
        {
            return BolnickoLecenjeServis.getInstance().nadjiBolnickoLecenjeZaPacijenta(idPacijenta);
        }
        public static void zavrsiBolnickoLecenje(String idPacijenta)
        {
            BolnickoLecenjeServis.getInstance().zavrsiBolnickoLecenje(idPacijenta);
        }

        public static void azurirajProstoriju(String idPacijenta, String idProstorije)
        {
            BolnickoLecenjeServis.getInstance().azurirajProstoriju(idPacijenta, idProstorije);
        }

        public static void azurirajTrajanje(String idProstorije, int trajanje)
        {
            BolnickoLecenjeServis.getInstance().azurirajTrajanje(idProstorije, trajanje);
        }

        public static bool proveriKrajBolnickogLecenje(String idPacijenta)
        {
            return BolnickoLecenjeServis.getInstance().proveriKrajBolnickogLecenje(idPacijenta);
        }

        public static List<BolnickoLecenje> ucitajSve()
        {
            return BolnickoLecenjeServis.getInstance().ucitajSve();
        }

        public static List<BolnickoLecenje> ucitajZaOdredjeniPeriod(DateTime pocetak, DateTime kraj)
        {
            return BolnickoLecenjeServis.getInstance().ucitajZaOdredjeniPeriod(pocetak, kraj);
        }
    }
}
