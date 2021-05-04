using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bolnica_aplikacija.Servis
{
    class SekretarServis
    {

        private SekretarRepozitorijum sekretarRepozitorijum = new SekretarRepozitorijum();
        private static PacijentRepozitorijum pacijentRepozitorijum = new PacijentRepozitorijum();
        private static KorisnikRepozitorijum korisnikRepozitorijum = new KorisnikRepozitorijum();
        private static AlergijaRepozitorijum alergijaRepozitorijum = new AlergijaRepozitorijum();
        private static TerminRepozitorijum terminRepozitorijum = new TerminRepozitorijum();
        private static PacijentServis pacijentServis = new PacijentServis();

        public String id { get; set; }
        public String idBolnice { get; set; }

       
    }
}
