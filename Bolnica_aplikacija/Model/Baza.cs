using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Bolnica_aplikacija.Model
{
    public class Baza
    {
        public static List<Pacijent> Pacijenti { get; set; }
        public static List<Lekar> Lekari { get; set; }
        public static List<Sekretar> Sekretari { get; set; }
        public static List<Upravnik> Upravnici { get; set; }

        public static List<Termin> Termini { get; set; }
        public static List<PomocnaKlasaKorisnici> Korisnici { get; set; }
    }
}
