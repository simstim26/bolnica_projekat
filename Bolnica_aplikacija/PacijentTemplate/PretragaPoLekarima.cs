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
    class PretragaPoLekarima : Pretraga
    {
        protected override List<PacijentTermin> filtrirajPoKriterijumu(string kriterijum)
        {
            List<PacijentTermin> termini = new List<PacijentTermin>();

            String[] podaci = kriterijum.Split(' ');

            foreach (Termin termin in TerminServis.getInstance().ucitajSve())
            {
                if (termin.idPacijenta.Equals(""))
                {

                    int rezultat = DateTime.Compare(termin.datum, DateTime.Today);

                    if (rezultat > 0)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();

                        popuniPodatke(termin, pacijentTermin, podaci);

                        if(!pacijentTermin.imeLekara.Equals("") && !pacijentTermin.lokacija.Equals(""))
                        {
                            termini.Add(pacijentTermin);
                        }
                    }
                }
            }
            return termini;
        }
    }
}
