using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class ProstorijaZauzetoServis
    {
        ProstorijaZauzetoRepozitorijum prostorijaZauzetoRepozitorijum = new ProstorijaZauzetoRepozitorijum();
        public List<ProstorijaZauzeto> ucitajSve()
        {
            return prostorijaZauzetoRepozitorijum.ucitajSve();
        }

        public void upisi(List<ProstorijaZauzeto> sveProstorije)
        {
            prostorijaZauzetoRepozitorijum.upisi(sveProstorije);
        }

        public void zauzmiProstorije()
        {
            var prostorijeZaZauzimanje = ucitajSve();
            var prostorije = ProstorijaKontroler.ucitajSve();
            
            foreach (ProstorijaZauzeto pz in prostorijeZaZauzimanje)
            {
                if (pz.jeZavrseno == false)
                {
                    foreach (Prostorija p in prostorije)
                    {
                        if (p.id == pz.idProstorije)
                        {
                            if (DateTime.Now >= pz.datumPocetka && DateTime.Now <= pz.datumKraja)
                            {
                                p.dostupnost = false;
                            }

                            /*if (DateTime.Now == pz.datumKraja)
                            {
                                pz.jeZavrseno = true;
                                p.dostupnost = true;
                            }*/
                        }
                    }
                 }

             }
            upisi(prostorijeZaZauzimanje);
            ProstorijaKontroler.upisi(prostorije);
        }
    }
}
