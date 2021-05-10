using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class GenerisanjeTerminaServis
    {

        public void generisiTermine()
        {
            //1. provera datuma
            //2. generisanje termina za svakog lekara u njegovom radnom vremenu; pretpostavka svi su ujutro 7-13
            //2a. pravljenje termina odmah i upis u datoteku
            
            if(DateTime.Now.Day == 10)
            {
                generisiTermineOP();
                generisiTermineSpec();
            }

        }

        private void generisiTermineOP()
        {
            String[] satnice = { "07:30", "08:00", "08:30", "09:00", "09:30", "10:30", "11:00", "11:30", "12:00", "12:30" };
            foreach(Lekar lekar in LekarServis.getInstance().ucitajSve().GroupBy(l => l.idSpecijalizacije)
                .ToDictionary(l => l.Key, l=> l.ToList())["0"].ToList())
            {
                int i = 1;
                while(i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 10)
                {
                    foreach(String sat in satnice)
                    {
                        /*(TipTermina tip, DateTime datum, DateTime satnica, bool jeZavrsen, string idTermina, string idProstorije,
            string idPacijenta, string idLekara, string idTerapije, string idBolesti, string izvestaj, string izvestajUputa,
            string idUputLekara, string idUputTermin, TipTermina tipUput, bool jeHitan*/
                        DateTime datum = DateTime.Now.AddDays(i);
                        String[] x = sat.Split(':');
                        DateTime satnica = datum.Date + new TimeSpan(Convert.ToInt32(x[0]), Convert.ToInt32(x[1]), 0);
                        TerminServis.getInstance().napraviTermin(new Termin(TipTermina.PREGLED, datum, satnica, false, "", "1", "", lekar.id,
                            null, null, null, null, null, null, TipTermina.PREGLED, false));
                    }
                    ++i;
                }
            }
        }

        private void generisiTermineSpec()
        {
            String[] satnice = { "07:30", "08:30",  "09:30", "11:00", "12:00"};
            foreach (Lekar lekar in LekarServis.getInstance().ucitajSve())
            {
                if (!lekar.idSpecijalizacije.Equals("0"))
                {
                    int i = 1;
                    while (i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 10)
                    {
                        foreach (String sat in satnice)
                        {
                            //potrebno ubaciti provere za operacije
                            DateTime datum = DateTime.Now.AddDays(i);
                            String[] x = sat.Split(':');
                            DateTime satnica = datum.Date + new TimeSpan(Convert.ToInt32(x[0]), Convert.ToInt32(x[1]), 0);
                            TerminServis.getInstance().napraviTermin(new Termin(TipTermina.PREGLED, datum, satnica, false, "", "1", "", lekar.id,
                                null, null, null, null, null, null, TipTermina.PREGLED, false));
                        }
                        ++i;
                    }
                }
            }
        }

    }
}
