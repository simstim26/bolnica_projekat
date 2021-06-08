using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class ObavestenjeDTO
    {
        public String id { get; set; }
        public String naslovObavestenja { get; set; }
        public String sadrzajObavestenja { get; set; }
        public bool jeLogickiObrisano { get; set; }

        //Ideja: Iterira kroz obavestenja i kada se ID notifikacije poklopi, prikaze ga

        public ObavestenjeDTO(String naslovObavestenja, String sadrzajObavestenja)
        {
            this.naslovObavestenja = naslovObavestenja;
            this.sadrzajObavestenja = sadrzajObavestenja;
            this.jeLogickiObrisano = false;
        }
    }
}
