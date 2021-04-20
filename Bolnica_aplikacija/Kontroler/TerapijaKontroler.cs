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
