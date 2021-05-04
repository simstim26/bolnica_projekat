using Bolnica_aplikacija.Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.PomocneKlase
{
    class PomocnaKlasaProvere
    {
        //ako je trenutan datum manji od datuma za uporediti onda je povratna vrednost false u suprotnom je true
        public static bool uporediDatumSaDanasnjim(DateTime datumZaUporediti)
        {
            bool povratnaVrednost = false;

            int rezultat = DateTime.Compare(DateTime.Now, datumZaUporediti.AddHours(1));

            if(rezultat <= 0)
            {
                povratnaVrednost = false;
            }
            else
            {
                povratnaVrednost = true;
            }

            return povratnaVrednost;
        }

        public static void antiTrolMetoda(String idPacijenta)
        {
            if (LogovanjeKontroler.proveriPostojanjeLogovanja(idPacijenta))
            {
                if (!LogovanjeKontroler.proveriVremePostojecegLogovanja(idPacijenta))
                {
                    //LogovanjeKontroler.resetujLogovanje(idPacijenta);
                    LogovanjeKontroler.uvecajBrojIzmena(idPacijenta);
                }
                
            }
            else
            {
                //ako ne postoji kreira se
                DateTime vremeIzmene = DateTime.Now;
                int brojUzastopnihIzmena = 1;
                LogovanjeKontroler.dodajLogovanje(new PomocneKlase.Logovanje(idPacijenta, vremeIzmene, brojUzastopnihIzmena));

            }
        }

        public static bool proveraPretrage(int indikator)
        {
            if (indikator == -1)
            {
                MessageBox.Show("Molimo izaberite prioritet pretrage.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;

            }
            else if (indikator == -2)
            {
                MessageBox.Show("Molimo unesite ime u odgovarajućem formatu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (indikator == -3)
            {
                MessageBox.Show("Molimo unesite datum u odgovarajućem formatu. Neki od podržanih formata su: dd/MM/yyyy, d/m/yyyy, dd.MM.yyyy.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
                return true;
            
        }

    }
}
