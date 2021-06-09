using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs.Implementacija
{
    class SpajaneRenoviranje : IRenoviranje
    {
        public void renoviraj(ProstorijaRenoviranje p)
        {
            Prostorija prostorija = ProstorijaKontroler.nadjiProstorijuPoId(p.idProstorije);
            Prostorija prostorijaDruga = ProstorijaKontroler.nadjiProstorijuPoId(p.idProstorijeKojaSeSpaja);
            ProstorijaKontroler.ObrisiProstoriju(prostorijaDruga.id);
            prostorija.Stavka = new List<Stavka>();
            ProstorijaKontroler.kopirajProstorijuIUpisi(prostorija);
        }
    }
}
