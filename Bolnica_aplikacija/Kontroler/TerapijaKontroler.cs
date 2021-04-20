using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class TerapijaKontroler
    {
        private static TerapijaServis terapijaServis = new TerapijaServis();

        public static void azurirajTerapiju(String idTerapije, String idLeka, String nacinUpotrebe, int trajanje
            , DateTime datumPropisivanja)
        {
            terapijaServis.azurirajTerapiju(idTerapije, idLeka, nacinUpotrebe, trajanje, datumPropisivanja);
        }

        public static Terapija nadjiTerapijuPoId(String idTerapije)
        {
            return terapijaServis.nadjiTerapijuPoId(idTerapije);
        }
        public static void dodajTerapiju(DateTime datumPropisivanja,int trajanje,String nacinUpotrebe,String idLeka,
           String idPacijenta)
        {
            Terapija terapija = new Terapija();
            terapija.datumPocetka = datumPropisivanja;
            terapija.idPacijenta = idPacijenta;
            terapija.nacinUpotrebe = nacinUpotrebe;
            terapija.trajanje = trajanje;
            terapija.idLeka = idLeka;
            terapija.idTermina = TerminKontroler.getTermin().idTermina;
            terapijaServis.dodajTerapiju(terapija);
        }
    }
}
