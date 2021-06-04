using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class PomocnaKlasaKalendar
    {
        public static void azurirajKalendar(MyCalendar.Calendar.Calendar calendar)
        {

            for (int i = 0; i < 42; i++)
            {
                calendar.Days[i].Notes = "";
            }

            popuniKalendar(calendar);

        }

        public static void popuniKalendar(MyCalendar.Calendar.Calendar calendar)
        {
            for (int i = 0; i < 42; i++)
                calendar.Days[i].Enabled = false;

            List<PacijentTermin> termini = TerminKontroler.pronadjiPacijentTerminUTrenutnomMesecu(KorisnikKontroler.GetPacijent().id);

            foreach (PacijentTermin pacijentTermin in termini)
            {
                DateTime datum = DateTime.Parse(pacijentTermin.datum);

                for (int i = 0; i < 42; i++)
                {
                    if (i == datum.Day)
                    {
                        calendar.Days[datum.Day + 1].Notes = pacijentTermin.napomena + "," + pacijentTermin.imeLekara;
                    }
                }
            }
        }

    }
}
