using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class Obavestenje
    {
        private string v;

        public String id { get; set; }
        public String naslovObavestenja { get; set; }
        public String sadrzajObavestenja { get; set; }
        public Notifikacija notifikacija { get; set; }
        public bool jeLogickiObrisano { get; set; }

        //Ideja: Iterira kroz obavestenja i kada se ID notifikacije poklopi, prikaze ga
        public Obavestenje(String id, String naslovObavestenja, String sadrzajObavestenja, Notifikacija notifikacija)
        {
            this.id = id;
            this.naslovObavestenja = naslovObavestenja;
            this.sadrzajObavestenja = sadrzajObavestenja;
            this.notifikacija = notifikacija;
            this.jeLogickiObrisano = false;
          
        }

    }
}
