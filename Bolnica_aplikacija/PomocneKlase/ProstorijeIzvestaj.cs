using Bolnica_aplikacija.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class ProstorijeIzvestaj
    {
        public Prostorija prostorija { get; set; }
        public List<Termin> termini { get; set; }
        public List<BolnickoLecenje> bolnickoLecenje { get; set; }

        public ProstorijeIzvestaj()
        {
        }

        public ProstorijeIzvestaj(Prostorija prostorija, List<BolnickoLecenje> bolnickoLecenje)
        {
            this.prostorija = prostorija;
            this.bolnickoLecenje = bolnickoLecenje;
        }

        public ProstorijeIzvestaj(Prostorija prostorija, List<Termin> termini)
        {
            this.prostorija = prostorija;
            this.termini = termini;
        }
    }
}
