using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class StavkaKontroler
    {
        private static StavkaServis stavkaServis = new StavkaServis();

        public static List<Stavka> UcitajSve()
        {
            return stavkaServis.UcitajSve();
        }

        public static List<Stavka> UcitajNeobrisaneStavke()
        {
            return stavkaServis.UcitajNeobrisaneStavke();
        }

        public static void IzbrisiStavku(Stavka stavka)
        {
            stavkaServis.IzbrisiStavku(stavka);
        }
        public static void Upisi(List<Stavka> sveStavke)
        {
            stavkaServis.Upisi(sveStavke);
        }

        public static void DodajStavku()
        {
            stavkaServis.DodajStavku();
        }

        public static void IzmeniStavku(Stavka stavka)
        {
            stavkaServis.IzmeniStavku(stavka);
        }

        public static Stavka pronadjiStavkuPoId(String id)
        {
            return stavkaServis.pronadjiStavkuPoId(id);
        }
    }
}
