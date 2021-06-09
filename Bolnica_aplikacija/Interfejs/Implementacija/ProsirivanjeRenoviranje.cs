using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Interfejs.Implementacija
{
    class ProsirivanjeRenoviranje : IRenoviranje
    {
        public void renoviraj(ProstorijaRenoviranje p)
        {
            Prostorija prostorija = ProstorijaKontroler.nadjiProstorijuPoId(p.idProstorije);
            ProstorijaDTO prostorijaNova = new ProstorijaDTO(ProstorijaKontroler.postaviIDProstorija(), prostorija.idBolnice, (ITipProstorije)prostorija.tipProstorije,
                prostorija.broj + "-a", prostorija.sprat, true, false, 0, null);
            ProstorijaKontroler.noviBrojSobe(prostorijaNova, prostorija);
        }
    }
}
