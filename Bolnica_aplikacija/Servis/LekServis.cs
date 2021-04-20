using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class LekServis
    {
        private LekRepozitorijum lekRepozitorijum = new LekRepozitorijum();

        public List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            List<Lek> povratnaVrednost = new List<Lek>();

            foreach(Lek lek in ucitajSve())
            {
                if (!idLeka.Equals(lek.id))
                {
                    povratnaVrednost.Add(lek);
                }
            }

            return povratnaVrednost;
        }
        public Lek nadjiLekPoId(String idLeka)
        {
            Lek povratnaVrednost = new Lek();

            foreach(Lek lek in ucitajSve())
            {
                if (idLeka.Equals(lek.id))
                {
                    povratnaVrednost.id = lek.id;
                    povratnaVrednost.kolicina = lek.kolicina;
                    povratnaVrednost.nacinUpotrebe = lek.nacinUpotrebe;
                    povratnaVrednost.naziv = lek.naziv;
                    povratnaVrednost.proizvodjac = lek.proizvodjac;
                    povratnaVrednost.tip = lek.tip;

                    break;
                }
            }

            return povratnaVrednost;
        }

        public List<Lek> ucitajSve()
        {
            return lekRepozitorijum.ucitajSve();
        }

    }
}
