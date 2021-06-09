using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PacijentTemplate
{
    class PretragaPoDatumima : Pretraga
    {
        protected override List<PacijentTermin> filtrirajPoKriterijumu(string kriterijum)
        {
            List<PacijentTermin> terminiSlobodni = new List<PacijentTermin>();
            try
            {
                radSaPodacimaPrilikomFiltriranja(terminiSlobodni, kriterijum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return terminiSlobodni;
        }

        protected override void radSaPodacimaPrilikomFiltriranja(List<PacijentTermin> termini, String kriterijum)
        {
            DateTime kriterijumPretrage = new DateTime();
            kriterijumPretrage = Convert.ToDateTime(kriterijum);

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {
                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);
                    int rezultatPretrage = DateTime.Compare(termin.datum, kriterijumPretrage);

                    if (rezultat > 0 && rezultatPretrage == 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        popuniPodatke(termin, pacijentTermin, null);
                        if(!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            termini.Add(pacijentTermin);
                        }
                    }
                }
            }
        }
    }
}
