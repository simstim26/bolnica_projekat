using Bolnica_aplikacija.Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void rbtnLekarCekiran(String tekst, int indikator)
        {
            if (!Regex.IsMatch(tekst, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                indikator = -2;
            }
            else
                indikator = 0;
        }

        public static void rbtnTerminCekiran(String tekst, int indikator)
        {
            var formati = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd.MM.yyyy", "dd.MM.yyyy.", "d.M.yyyy.", "d.M.yyyy" };
            DateTime dt;
            if (DateTime.TryParseExact(tekst, formati, null, System.Globalization.DateTimeStyles.None, out dt))
            {
                indikator = 1;
            }
            else
                indikator = -3;
        }

        public static int prebrojTermine(String idPacijenta, int mesec)
        {
            int brojTermina = 0;

            brojTermina = TerminKontroler.pronadjiOdradjeneTermineZaMesec(idPacijenta, mesec);

            return brojTermina;
        }


    }
}
